using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace RoverSurMars
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenOrientationIsNordWhenAvancerThenYBecomes1()
        {
            var rover = new Rover(0, 0, Orientation.Nord);

            rover.Avancer();

            Check.That(rover.X).IsEqualTo(0);
            Check.That(rover.Y).IsEqualTo(1);
            Check.That(rover.Orientation).IsEqualTo(Orientation.Nord);
        }

        [TestMethod]
        public void GivenOrientationIsSuddWhenAvancerThenYBecomesMinus1()
        {
            var rover = new Rover(0, 0, Orientation.Sud);

            rover.Avancer();

            Check.That(rover.X).IsEqualTo(0);
            Check.That(rover.Y).IsEqualTo(-1);
            Check.That(rover.Orientation).IsEqualTo(Orientation.Sud);
        }

        [TestMethod]
        public void GivenOrientationIsEstWhenAvancerThenxBecomesMinus1()
        {
            var rover = new Rover(0, 0, Orientation.Est);

            rover.Avancer();

            Check.That(rover.X).IsEqualTo(-1);
            Check.That(rover.Y).IsEqualTo(0);
            Check.That(rover.Orientation).IsEqualTo(Orientation.Est);
        }

        [TestMethod]
        public void GivenOrientationIsOuestWhenAvancerThenXBecomes1()
        {
            var rover = new Rover(0, 0, Orientation.Ouest);

            rover.Avancer();

            Check.That(rover.X).IsEqualTo(1);
            Check.That(rover.Y).IsEqualTo(0);
            Check.That(rover.Orientation).IsEqualTo(Orientation.Ouest);
        }
    }

    public class Rover
    {
        private readonly MouvementBase[] _mouvements;

        public Rover(int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            _mouvements = new MouvementBase[]
            {
                new Monte(this),
                new Descends(this), 
                new Gauche(this), 
                new Droite(this)
            };
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Orientation Orientation { get; private set; }

        public void Avancer()
        {
            _mouvements.First(m => m.CanMove()).Move();
        }

        public void Monte()
        {
            Y++;
        }

        public void Descends()
        {
            Y--;
        }

        public void Gauche()
        {
            X--;
        }

        public void Droite()
        {
            X++;
        }
    }

    public abstract class MouvementBase
    {
        private readonly Orientation _mouvementOrientation;

        protected MouvementBase(Rover rover, Orientation mouvementOrientation)
        {
            Rover = rover;
            _mouvementOrientation = mouvementOrientation;
        }

        public Rover Rover { get; set; }

        public bool CanMove()
        {
            return Rover.Orientation == _mouvementOrientation;
        }

        public abstract void Move();
    }

    public enum PivotementDirection
    {
        Gauche,
        Droite
    }

    public class Monte : MouvementBase
    {
        public Monte(Rover rover)
            : base(rover, Orientation.Nord)
        { }

        public override void Move()
        {
            Rover.Monte();
        }
    }

    public class Descends : MouvementBase
    {
        public Descends(Rover rover)
            : base(rover, Orientation.Sud)
        { }

        public override void Move()
        {
            Rover.Descends();
        }
    }

    public class Gauche : MouvementBase
    {
        public Gauche(Rover rover)
            : base(rover, Orientation.Est)
        { }

        public override void Move()
        {
            Rover.Gauche();
        }
    }

    public class Droite : MouvementBase
    {
        public Droite(Rover rover)
            : base(rover, Orientation.Ouest)
        {
        }

        public override void Move()
        {
            Rover.Droite();
        }
    }

    public enum Orientation
    {
        Nord,
        Sud,
        Est,
        Ouest
    }

    public class CircularList<T>
    {
        private readonly List<T> _list;

        public CircularList(params T[] list)
        {
            _list = new List<T>(list);
        }

        public T Get(T current, int offset)
        {
            var index = _list.FindIndex(o => o.Equals(current)) + offset;

            throw new NotImplementedException();
        }
    }
}
