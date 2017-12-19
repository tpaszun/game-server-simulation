package com.yggdrasilAssignment;

public class RewardExt {
    public static int Value(Reward reward) {
        switch(reward) {
            case Hundred:
                return 100;
            case Twenty:
                return 20;
            case Five:
                return 5;
            default:
                return 0;
        }
    }
}
