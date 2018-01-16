package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.AdditionalRewardExt;
import com.yggdrasilAssignment.RewardExt;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class GameWithProbability {
    private Fraction _probability;
    private final List<Move> _moves;

    public GameWithProbability() {
        _probability = new Fraction(1, 1);
        _moves = new ArrayList<>();
    }

    public GameWithProbability(GameWithProbability other) {
        _probability = other.getProbability();
        _moves = new ArrayList<>(other.getMoves());
    }

    public Fraction getProbability() {
        return _probability;
    }

    public List<Move> getMoves() {
        return _moves;
    }

    public void Add(Move move, Fraction probability) {
        _moves.add(move);
        _probability = _probability.multiply(probability);
    }

    public int score() {
        int score = 0;
        for(Move move: _moves) {
            if (move instanceof RewardMove) {
                score += RewardExt.Value(((RewardMove)move).getReward());
            } else if (move instanceof AdditionalRewardMove) {
                score += AdditionalRewardExt.Value(((AdditionalRewardMove)move).getAdditionalReward());
            }
        }
        return score;
    }

    public String toString() {
        return String.format("Probability: %s, Moves: %s",
                _probability.toString(),
                String.join(", ", _moves.stream().map(Object::toString).collect(Collectors.toList())));
    }
}
