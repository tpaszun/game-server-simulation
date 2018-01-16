package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.Reward;

public class Box {
    private int _count;
    private final Reward _reward;

    public Box(Reward reward, int count) {
        _reward = reward;
        _count = count;
    }

    public Box(Box other) {
        _reward = other.getReward();
        _count = other.getCount();
    }

    public Reward getReward() {
        return _reward;
    }

    public int getCount() {
        return _count;
    }

    public void decrementCount() {
        _count--;
    }
}
