package com.yggdrasilAssignment;

public class AdditionalRewardExt {
    public static int Value(AdditionalReward additionalReward) {
        switch(additionalReward) {
            case Twenty:
                return 20;
            case Ten:
                return 10;
            case Five:
                return 5;
            default:
                return 0;
        }
    }
}
