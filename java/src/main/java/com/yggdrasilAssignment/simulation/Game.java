package com.yggdrasilAssignment.simulation;

import com.yggdrasilAssignment.AdditionalReward;
import com.yggdrasilAssignment.AdditionalRewardExt;
import com.yggdrasilAssignment.Reward;
import com.yggdrasilAssignment.RewardExt;

import java.security.SecureRandom;
import java.util.*;

import static java.util.Collections.*;

public class Game {
    private final Queue<Reward> _boxes;
    private final Queue<AdditionalReward> _additionalRewards;

    private boolean _gotExtraLife;
    private int _score;

    public Game(Collection<Reward> rewards, Collection<AdditionalReward> additionalRewards) {
        List<Reward> rewardList = new ArrayList<>(rewards);
        shuffle(rewardList, new SecureRandom());
        _boxes = new LinkedList<>(rewardList);

        List<AdditionalReward> additionalRewardList = new ArrayList<>(additionalRewards);
        shuffle(additionalRewardList, new SecureRandom());
        _additionalRewards = new LinkedList<>(additionalRewardList);
    }

    public int Play() {
        OpenBoxes();
        SelectAdditionalReward();

        return _score;
    }

    private void OpenBoxes() {
        while (!_boxes.isEmpty()) {
            Reward box = _boxes.remove();

            if (box == Reward.GameOver) {
                if (_gotExtraLife) {
                    _gotExtraLife = false;
                    continue;
                }
                else
                    break;
            }

            switch(box) {
                case Hundred:
                case Twenty:
                case Five:
                    _score += RewardExt.Value(box);
                    break;
                case ExtraLife:
                    _gotExtraLife = true;
                    break;
            }
        }
    }

    private void SelectAdditionalReward() {
        AdditionalReward additionalReward = _additionalRewards.remove();

        switch(additionalReward) {
            case Twenty:
            case Ten:
            case Five:
                _score += AdditionalRewardExt.Value(additionalReward);
                break;
            case SecondChance:
                Play();
                break;
        }
    }
}
