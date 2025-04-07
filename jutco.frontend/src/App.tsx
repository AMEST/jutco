import './App.css';
import CodeMirrorEditor from './components/CodeMirrorEditor';

function App() {
  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl mb-4">Junior Text Consultant (JuTCo)</h1>
      <CodeMirrorEditor />
    </div>
  );
}

export default App;
