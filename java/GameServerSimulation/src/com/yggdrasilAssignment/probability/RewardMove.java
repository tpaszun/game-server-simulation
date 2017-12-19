package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.Reward;

public class RewardMove extends Move {
    private Reward _reward;

    public Reward getReward() { return _reward; }

    public RewardMove(Reward reward) {
            _reward = reward;
    }

    public String toString() {
        return _reward.toString();
    }
}

