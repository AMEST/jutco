import React, { useRef, useState, useMemo } from 'react';
import CodeMirror, { BasicSetupOptions, EditorView, ReactCodeMirrorRef } from '@uiw/react-codemirror';
import { ReviewResult } from '../models/ReviewResult';
import { Decoration, DecorationSet } from '@codemirror/view';
import { StateField, StateEffect } from '@codemirror/state';

const highlightField = StateField.define<DecorationSet>({
  create() {
    return Decoration.none;
  },
  update(decorations, tr) {
    const effects = tr.effects.filter(e => e.is(highlightEffect));
    if (!effects.length) return decorations;
    
    return Decoration.set(
      effects.flatMap(effect => 
        effect.value.map((f: {from: number, to: number}) =>
          Decoration.mark({
            class: 'highlight-error',
            attributes: { style: 'background-color: rgba(255, 0, 0, 0.2)' }
          }).range(f.from, f.to)
        )
      )
    );
  }
});

const highlightEffect = StateEffect.define<Array<{from: number, to: number}>>();

const CodeMirrorEditor: React.FC = () => {
  const [code, setCode] = useState<string>('Вставьте текст для проверки');
  const [reviewResult, setReviewResult] = useState<ReviewResult>({score: 0, fragments: []});
  const [reviewDebounceId, setReviewDebounceId] = useState<NodeJS.Timeout>();
  const codemirror = useRef<ReactCodeMirrorRef>({});

  const highlightExtensions = useMemo(() => [
    highlightField,
    EditorView.decorations.from(highlightField)
  ], []);
  const codeMirrorConfig : BasicSetupOptions = {
    lineNumbers: false,
    highlightActiveLine: false,
    highlightSelectionMatches: false
  };

  const handleChange = async (value: string) => {
    setCode(value);
    clearTimeout(reviewDebounceId);
    setReviewDebounceId(setTimeout(() => handleReview(value), 2000));
  };

  const handleReview = async (text: string) => {
    if(!text)
        return;
    const response = await fetch('/api/Review', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ "text" : text }),
    });
    const result : ReviewResult = await response.json();
    setReviewResult(result);
    
    if (codemirror.current.view) {
      const ranges = result.fragments
        .map(f => ({
          from: f.start,
          to: f.end
        }))
        .sort((a, b) => a.from - b.from);
      codemirror.current.view.dispatch({
        effects: highlightEffect.of(ranges)
      });
    }
  };

  return (
    <div>
      <CodeMirror
        ref={codemirror}
        value={code}
        theme={'light'}
        minHeight="400px"
        height="400px"
        basicSetup={codeMirrorConfig}
        extensions={[EditorView.lineWrapping, ...highlightExtensions]}
        onChange={handleChange}
      />
      <div className="score-block">
        <div className='controls'>
          <div className='score-number'>{reviewResult.score}</div>
          <div className='score-caption'>баллов из 10</div>
        </div>
        <div className='problems-list problem-list-mw'>
          {Array.from(new Set(reviewResult?.fragments.map(f => f.hint.name))).map(name => (
            <a 
              key={name}
              className="problem-link"
              onClick={() => {
                const element = document.getElementById(name);
                if (element) {
                  element.scrollIntoView({ behavior: 'smooth' });
                }
              }}
            >
              {name}
            </a>
          ))}
        </div>
      </div>
      <div>
        {Object.entries(
          reviewResult?.fragments.reduce((groups, fragment) => {
            const group = fragment.hint.name;
            if (!groups[group]) {
              groups[group] = [];
            }
            groups[group].push(fragment);
            return groups;
          }, {} as {[key: string]: typeof reviewResult.fragments})
        ).map(([hintName, fragments]) => (
          <div key={hintName} id={hintName} className="problem-group">
            <h3 className="problem-group-title">{hintName}</h3>
            <ul className="problem-group-list">
              {fragments.map((fragment, index) => (
                <li key={index}>
                  {code.substring(fragment.start, fragment.end)} - {fragment.hint.shortDescription}
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CodeMirrorEditor;
