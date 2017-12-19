using System;
using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Probability;
using Xunit;

namespace GameServerSimulation.Tests.Probability
{
    public class BoxesTests
    {
        private Boxes _boxes = new Boxes(new List<Box> {
            new Box(Reward.Hundred, 1),
            new Box(Reward.Twenty, 2),
            new Box(Reward.Five, 5)
        });

        [Fact]
        public void CountBoxes()
        {
            Assert.Equal(8, _boxes.CountBoxes());
        }

        [Fact]
        public void RemoveBox()
        {
            _boxes.RemoveBox(Reward.Five);

            Assert.Equal(7, _boxes.CountBoxes());
            Assert.Contains(_boxes, box => box.Reward == Reward.Five);
            var fiveBox = _boxes.Single(box => box.Reward == Reward.Five);
            Assert.Equal(4, fiveBox.Count);

            _boxes.RemoveBox(Reward.Hundred);

            Assert.Equal(6, _boxes.CountBoxes());
            Assert.DoesNotContain(_boxes, box => box.Reward == Reward.Hundred);
        }
    }
}