package com.yggdrasilAssignment.probability;

import com.yggdrasilAssignment.Reward;

import java.util.Iterator;
import java.util.List;
import java.util.stream.Collectors;

public class Boxes implements Iterable<Box> {
    private List<Box> _boxes;

    public Boxes(List<Box> items) {
        _boxes = items;
    }

    public Boxes(Boxes other) {
        _boxes = other._boxes.stream().map(b -> new Box(b)).collect(Collectors.toList());
    }

    public void removeBox(Reward reward) {
        Box box = _boxes.stream()
                .filter(b -> b.getReward() == reward)
                .findFirst()
                .get();

        if (box.getCount() > 1) {
            box.decrementCount();
        } else {
            _boxes.remove(box);
        }
    }

    public int countBoxes() {
        int count = 0;
        for(Box box: _boxes)
            count += box.getCount();
        return count;
    }

    @Override
    public Iterator<Box> iterator() {
        return _boxes.iterator();
    }
}
