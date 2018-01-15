package com.yggdrasilAssignment;

import com.yggdrasilAssignment.probability.Fraction;
import com.yggdrasilAssignment.probability.GameGenerator;
import com.yggdrasilAssignment.probability.GameWithProbability;
import com.yggdrasilAssignment.simulation.SimulationDriver;

import java.util.*;

public class Main {

    public static void main(String[] args) {
        System.out.println("Average score by probability");
        System.out.println("============================");
        Collection<GameWithProbability> gamesWithProbability = Probability();
        System.out.println();
        System.out.println();
        System.out.println("Average score by simulation");
        System.out.println("===========================");
        int gamesCount = 150_000_000;
        Dictionary<Integer, Long> simulationStats = Simulation(gamesCount);
        System.out.println();
        System.out.println();
        System.out.println("Check simulation results with probability calculation");
        System.out.println("=====================================================");
        CheckResults(gamesCount, gamesWithProbability, simulationStats);
    }

    private static Collection<GameWithProbability> Probability() {
        GameGenerator generator = new GameGenerator();

        generator.Generate();

        System.out.format("Total games: %d\n", generator.getGames().size());

        Fraction totalProbability = generator.getGames()
                .stream()
                .map(GameWithProbability::getProbability)
                .reduce(new Fraction(0, 1), Fraction::add);

        Fraction averageScore = generator.getGames()
                .stream()
                .map(g -> g.getProbability().multiply(new Fraction(g.score(), 1)))
                .reduce(new Fraction(0, 1), Fraction::add);

        System.out.format("Total probability: %s (should be 1)\n", totalProbability);
        System.out.format("Average score: %s\n", averageScore);

        return generator.getGames();
    }

    private static Dictionary<Integer, Long> Simulation(int gamesCount) {
        Collection<Reward> rewards = Arrays.asList(
            Reward.Hundred,
            Reward.Twenty,
            Reward.Twenty,
            Reward.Five,
            Reward.Five,
            Reward.Five,
            Reward.Five,
            Reward.Five,
            Reward.ExtraLife,
            Reward.GameOver,
            Reward.GameOver,
            Reward.GameOver);

        Collection<AdditionalReward> additionalRewards = Arrays.asList(
            AdditionalReward.Twenty,
            AdditionalReward.Ten,
            AdditionalReward.Five,
            AdditionalReward.SecondChance);

        SimulationDriver simulationDriver = new SimulationDriver(gamesCount, rewards, additionalRewards);

        double result = simulationDriver.Run(500_000, (progress, averageScore) ->
                System.out.format("Played %d games, average score: %f\n", progress, averageScore));
        System.out.format("Average score after %d games: %f", gamesCount, result);

        return simulationDriver.getScoreStatistics();
    }

    private static void CheckResults(int totalGames, Collection<GameWithProbability> probabilityGames, Dictionary<Integer, Long> simulationStats) {
        Hashtable<Integer, Fraction> scoreProbability = new Hashtable<>();

        for(GameWithProbability game: probabilityGames) {
            int score = game.score();
            if (!scoreProbability.containsKey(score))
                scoreProbability.put(score, game.getProbability());
            else {
                Fraction newProbability = game.getProbability().add(scoreProbability.get(score));
                scoreProbability.put(score, newProbability);
            }
        }

        Collection<ScoreStats> stats = new ArrayList<>();

        for(Integer score: scoreProbability.keySet()) {
            stats.add(new ScoreStats(score, scoreProbability.get(score), totalGames, simulationStats.get(score)));
        }

        double maxError = 0;

        for(ScoreStats s: stats) {
            maxError = s.Error > maxError ? s.Error : maxError;
        }

        System.out.format("Maximal error: %f%%", maxError);
    }
}

class ScoreStats {
    public final int Score;
    public final long SimulationCount;
    public final long ProbableCount;
    public final double Error;


    public ScoreStats(int score, Fraction probability, int totalGames, long simulationScoreCount) {
        Score = score;
        SimulationCount = simulationScoreCount;
        ProbableCount = (long)probability.multiply(new Fraction(totalGames, 1)).toDouble();
        Error = new Fraction(Math.abs(ProbableCount - SimulationCount), ProbableCount).toDouble() * 100;
    }
}
