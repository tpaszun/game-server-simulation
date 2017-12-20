package com.yggdrasilAssignment.simulation;

import com.yggdrasilAssignment.AdditionalReward;
import com.yggdrasilAssignment.AdditionalRewardExt;
import com.yggdrasilAssignment.Reward;
import com.yggdrasilAssignment.RewardExt;

import java.security.SecureRandom;
import java.util.*;

public class Game {
    private Queue<Reward> _boxes;
    private Queue<AdditionalReward> _additionalRewards;
    private boolean _gotExtraLife;
    private int _score;

    public Game() {
        List<Reward> boxes = Arrays.asList(
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
        Collections.shuffle(boxes, new SecureRandom());
        _boxes = new LinkedList<>(boxes);

        List<AdditionalReward> additionalRewards = Arrays.asList(
                AdditionalReward.Twenty,
                AdditionalReward.Ten,
                AdditionalReward.Five,
                AdditionalReward.SecondChance);
        Collections.shuffle(additionalRewards, new SecureRandom());
        _additionalRewards = new LinkedList<>(additionalRewards);
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
