export interface SeasonGames {
    id: number;
    name: string;
    games: Game[];
}

export interface Game {
    id: number;
    description: string;
    seasonId: number;
}