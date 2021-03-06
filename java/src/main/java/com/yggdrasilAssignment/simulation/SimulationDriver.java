package com.yggdrasilAssignment.simulation;

import com.yggdrasilAssignment.AdditionalReward;
import com.yggdrasilAssignment.Reward;

import java.util.Collection;
import java.util.Collections;
import java.util.Dictionary;
import java.util.Hashtable;

import static java.util.Collections.*;

public class SimulationDriver {
    private final int _gamesCount;
    private final Collection<Reward> _rewards;
    private final Collection<AdditionalReward> _additionalRewards;

    private final Hashtable<Integer, Long> _scoreStatistics;

    public Dictionary<Integer, Long> getScoreStatistics() { return _scoreStatistics; }

    public SimulationDriver(int gamesCount, Collection<Reward> rewards, Collection<AdditionalReward> additionalRewards) {
        _gamesCount = gamesCount;
        _rewards = unmodifiableCollection(rewards);
        _additionalRewards = unmodifiableCollection(additionalRewards);

        _scoreStatistics = new Hashtable<>();
    }

    public double Run(int interval, SimulationProgress progress) {
        long scoresSum = 0;

        for (int i = 0; i < _gamesCount; i++)
        {
            Game game = new Game(_rewards, _additionalRewards);
            int score = game.Play();
            scoresSum += score;


            if (!_scoreStatistics.containsKey(score))
                _scoreStatistics.put(score, 1L);
            else
                _scoreStatistics.put(score, _scoreStatistics.get(score) + 1);

            if (i % interval == 0)
                progress.action(i, (double)scoresSum / i);
        }

        return (double)(scoresSum) / _gamesCount;
    }
}

