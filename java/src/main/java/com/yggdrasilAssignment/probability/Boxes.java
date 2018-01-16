package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.Reward;

import java.util.Iterator;
import java.util.List;
import java.util.Optional;

import static java.util.stream.Collectors.toList;

public class Boxes implements Iterable<Box> {
    private final List<Box> _boxes;

    public Boxes(List<Box> items) {
        _boxes = items;
    }

    public Boxes(Boxes other) {
        _boxes = other._boxes.stream().map(Box::new).collect(toList());
    }

    public void removeBox(Reward reward) {
        Optional<Box> boxMaybe = _boxes.stream()
                .filter(b -> b.getReward() == reward)
                .findFirst();

        if (!boxMaybe.isPresent())
            return; // ToDo: Should throw fatal exception and report

        Box box = boxMaybe.get();

        if (box.getCount() > 1) {
            box.decrementCount();
        } else {
            _boxes.remove(box);
        }
    }

    public int countBoxes() {
        return _boxes.stream().mapToInt(Box::getCount).sum();
    }

    @Override
    public Iterator<Box> iterator() {
        return _boxes.iterator();
    }
}
