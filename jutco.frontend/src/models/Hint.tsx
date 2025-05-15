export type Hint = {
    description: string,
    name: string,
    shortDescription: string,
    tab: HintTab,
    style: HintTab
}

export type HintTab = {
    color: string
    name: string
}
