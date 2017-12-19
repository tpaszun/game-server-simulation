package com.yggdrasilAssignment;

import com.yggdrasilAssignment.probability.Fraction;
import com.yggdrasilAssignment.probability.GameGenerator;
import com.yggdrasilAssignment.probability.GameWithProbability;
import com.yggdrasilAssignment.simulation.Game;

public class Main {

    public static void main(String[] args) {
        System.out.println("Average score by probability");
        System.out.println("============================");
        Probability();
        System.out.println();
        System.out.println();
        System.out.println("Average score by simulation");
        System.out.println("===========================");
        Simulation();
    }

    private static void Probability() {
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
    }

    private static void Simulation() {
        int gamesCount = 10000000;

        long scoresSum = 0;

        for (int i = 0; i < gamesCount; i++)
        {
            Game game = new Game();
            int score = game.Play();

            scoresSum += score;

            if (i % 500000 == 0)
                System.out.format("Played %d games, average score: %f\n", i, (double)scoresSum / i);
        }

        double avg = (double)(scoresSum) / gamesCount;
        System.out.format("Average score: %f", avg);
    }
}
