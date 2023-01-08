namespace AdventOfCode._2022;

public class Day2 : Puzzle
{
    public Day2(string instructions) : base(instructions)
    {
        OpponentMovesMapping = new Dictionary<char, RockPaperScissors>
        {
            ['A'] = RockPaperScissors.Rock,
            ['B'] = RockPaperScissors.Paper,
            ['C'] = RockPaperScissors.Scissors
        };

        PlayerMovesMapping = new Dictionary<char, RockPaperScissors>
        {
            ['X'] = RockPaperScissors.Rock,
            ['Y'] = RockPaperScissors.Paper,
            ['Z'] = RockPaperScissors.Scissors
        };

        RoundResults = new Dictionary<char, RoundResult>
        {
            ['X'] = RoundResult.Lose,
            ['Y'] = RoundResult.Draw,
            ['Z'] = RoundResult.Win
        };

        Rounds = InstructionLines
            .Map(instruction => new GameRound(opponentMove: OpponentMovesMapping[instruction[0]],
                                              mysterySymbol: instruction[2]));
    }

    private Dictionary<char, RockPaperScissors> OpponentMovesMapping { get; }
    private Dictionary<char, RockPaperScissors> PlayerMovesMapping { get; }
    private Dictionary<char, RoundResult> RoundResults { get; }
    private IEnumerable<GameRound> Rounds { get; }

    public override int Solve_Part1()
    {
        return Rounds
            .Select(round => round with { PlayerMove = PlayerMovesMapping[round.MysterySymbol] })
            .Sum(round => round.CalculateScore().Value);
    }

    public override int Solve_Part2()
    {
        return Rounds
            .Select(round =>
            {
                var outcome = RoundResults[round.MysterySymbol];
                return round with { Outcome = outcome, PlayerMove = round.OpponentMove.CalculatePlayerMove(outcome) };
            })
            .Sum(round => round.CalculateScore().Value);
    }
}

public enum RockPaperScissors { Rock = 1, Paper = 2, Scissors = 3 }
public enum RoundResult { Lose = 0, Draw = 3, Win = 6 }
public record Score(int Value);
public record GameRound
{
    public GameRound(RockPaperScissors opponentMove, char mysterySymbol)
    {
        OpponentMove = opponentMove;
        MysterySymbol = mysterySymbol;
    }
    public RockPaperScissors OpponentMove { get; }
    public char MysterySymbol { get; }  // instructions parsed once, this is reused in Part2
    public RockPaperScissors? PlayerMove { get; init; }
    public RoundResult? Outcome { get; init; }
};

public static class Game
{
    public static Score CalculateScore(this GameRound gameRound) =>
    (gameRound.PlayerMove, gameRound.OpponentMove) switch
    {
        (RockPaperScissors.Rock, RockPaperScissors.Rock) =>
            new(((int)RoundResult.Draw) + ((int)RockPaperScissors.Rock)),
        (RockPaperScissors.Rock, RockPaperScissors.Paper) =>
            new(((int)RoundResult.Lose) + ((int)RockPaperScissors.Rock)),
        (RockPaperScissors.Rock, RockPaperScissors.Scissors) =>
            new(((int)RoundResult.Win) + ((int)RockPaperScissors.Rock)),

        (RockPaperScissors.Paper, RockPaperScissors.Rock) =>
            new(((int)RoundResult.Win) + ((int)RockPaperScissors.Paper)),
        (RockPaperScissors.Paper, RockPaperScissors.Paper) =>
            new(((int)RoundResult.Draw) + ((int)RockPaperScissors.Paper)),
        (RockPaperScissors.Paper, RockPaperScissors.Scissors) =>
            new(((int)RoundResult.Lose) + ((int)RockPaperScissors.Paper)),

        (RockPaperScissors.Scissors, RockPaperScissors.Rock) =>
            new(((int)RoundResult.Lose) + ((int)RockPaperScissors.Scissors)),
        (RockPaperScissors.Scissors, RockPaperScissors.Paper) =>
            new(((int)RoundResult.Win) + ((int)RockPaperScissors.Scissors)),
        (RockPaperScissors.Scissors, RockPaperScissors.Scissors) =>
            new(((int)RoundResult.Draw) + ((int)RockPaperScissors.Scissors)),

        _ => throw new NotImplementedException(),
    };

    public static RockPaperScissors CalculatePlayerMove(this RockPaperScissors opponentChoice, RoundResult desiredOutcome) =>
        (opponentChoice, desiredOutcome) switch
        {
            (RockPaperScissors.Rock, RoundResult.Lose) => RockPaperScissors.Scissors,
            (RockPaperScissors.Rock, RoundResult.Draw) => RockPaperScissors.Rock,
            (RockPaperScissors.Rock, RoundResult.Win) => RockPaperScissors.Paper,
            (RockPaperScissors.Paper, RoundResult.Lose) => RockPaperScissors.Rock,
            (RockPaperScissors.Paper, RoundResult.Draw) => RockPaperScissors.Paper,
            (RockPaperScissors.Paper, RoundResult.Win) => RockPaperScissors.Scissors,
            (RockPaperScissors.Scissors, RoundResult.Lose) => RockPaperScissors.Paper,
            (RockPaperScissors.Scissors, RoundResult.Draw) => RockPaperScissors.Scissors,
            (RockPaperScissors.Scissors, RoundResult.Win) => RockPaperScissors.Rock,
            _ => throw new NotImplementedException(),
        };
}