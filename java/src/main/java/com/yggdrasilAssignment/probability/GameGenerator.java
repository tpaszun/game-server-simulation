package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.AdditionalReward;
import com.yggdrasilAssignment.Reward;

import java.util.*;

public class GameGenerator {
    private List<GameWithProbability> _games = new LinkedList<>();

    public void Generate()
    {
        Boxes boxes = new Boxes(Arrays.asList(
                new Box(Reward.Hundred, 1),
                new Box(Reward.Twenty, 2),
                new Box(Reward.Five, 5),
                new Box(Reward.ExtraLife, 1),
                new Box(Reward.GameOver, 3)));

        Collection<AdditionalReward> additionalRewards = Arrays.asList(
                AdditionalReward.Five,
                AdditionalReward.Ten,
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance);

        Generate(new GameWithProbability(), boxes, additionalRewards);
    }

    public void Generate(Boxes boxes, Collection<AdditionalReward> additionalRewards)
    {
        Generate(new GameWithProbability(), boxes, additionalRewards);
    }

    public Collection<GameWithProbability> getGames() {
        return _games;
    }

    private void Generate(GameWithProbability currentGame, Boxes boxes, Collection<AdditionalReward> additionalRewards)
    {
        for(Box box: boxes)
        {
            GameWithProbability game = new GameWithProbability(currentGame);
            Fraction moveProbability = new Fraction(box.getCount(), boxes.countBoxes());
            game.Add(new RewardMove(box.getReward()), moveProbability);

            Boxes newBoxes = new Boxes(boxes);
            newBoxes.removeBox(box.getReward());

            if (box.getReward() == Reward.GameOver && !HasExtraLife(game.getMoves()))
            {
                HandleAdditionalRewards(game, newBoxes, additionalRewards);
                continue;
            }

            Generate(game, newBoxes, additionalRewards);
        }
    }

    private void HandleAdditionalRewards(GameWithProbability currentGame, Boxes boxes, Collection<AdditionalReward> additionalRewards)
    {
        for(AdditionalReward additionalReward: additionalRewards)
        {
            GameWithProbability game = new GameWithProbability(currentGame);
            game.Add(new AdditionalRewardMove(additionalReward), new Fraction(1, additionalRewards.size()));

            if (additionalReward == AdditionalReward.SecondChance)
            {
                Collection<AdditionalReward> newAdditionalRewards = new ArrayList<>(additionalRewards);
                newAdditionalRewards.remove(additionalReward);

                Generate(game, boxes, newAdditionalRewards);
            }
            else
            {
                _games.add(game);
            }
        }
    }

    private boolean HasExtraLife(Collection<Move> moves)
    {
        return moves.stream().map(m -> {
           if (m instanceof RewardMove) {
               switch(((RewardMove)m).getReward()) {
                   case ExtraLife: return 1;
                   case GameOver: return -1;
               }
           }
           else if (m instanceof AdditionalRewardMove) {
               switch(((AdditionalRewardMove)m).getAdditionalReward()) {
                   case SecondChance: return 1;
               }
           }
           return 0;
        }).reduce(0, Integer::sum) >= 0;
    }
}
