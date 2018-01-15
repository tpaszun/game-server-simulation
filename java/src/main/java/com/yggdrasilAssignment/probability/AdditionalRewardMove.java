package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.AdditionalReward;

public class AdditionalRewardMove extends Move {
    private AdditionalReward _additionalReward;

    public AdditionalReward getAdditionalReward() { return _additionalReward; }

    public AdditionalRewardMove(AdditionalReward additionalReward) {
        _additionalReward = additionalReward;
    }

    public String toString() {
        return _additionalReward.toString();
    }
}
