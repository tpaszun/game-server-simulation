using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameServerSimulation.Probability
{
    public class Boxes : IEnumerable<Box>
    {
        private List<Box> _boxes = new List<Box>();

        public Boxes(List<Box> boxes)
        {
            _boxes = boxes;
        }

        public Boxes(Boxes other)
        {
            _boxes = other.Select(b => new Box(b)).ToList();
        }

        public IEnumerator<Box> GetEnumerator() => _boxes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _boxes.GetEnumerator();

        public void RemoveBox(Reward reward)
        {
            var box = _boxes.Single(b => b.Reward == reward);
            if (box.Count > 1)
                box.Count = box.Count - 1;
            else
                _boxes.Remove(box);
        }

        public int CountBoxes() => _boxes.Select(b => b.Count).Sum();
    }
}