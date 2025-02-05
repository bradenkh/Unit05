using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A long limbless reptile.</para>
    /// <para>The responsibility of Cycle is to move itself.</para>
    /// </summary>
    public class Cycle : Actor
    {
        private List<Actor> segments = new List<Actor>();
        private Color color = Constants.WHITE;
        private Boolean isAlive = true;

        /// <summary>
        /// Constructs a new instance of a Cycle.
        /// </summary>
        public Cycle(int player)
        {
            PrepareBody(player);
        }

        /// <summary>
        /// Gets the Cycle's body segments.
        /// </summary>
        /// <returns>The body segments in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(segments.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the Cycle's head segment.
        /// </summary>
        /// <returns>The head segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return segments[0];
        }

        /// <summary>
        /// Gets the Cycle's segments (including the head).
        /// </summary>
        /// <returns>A list of Cycle segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return segments;
        }

        /// <summary>
        /// Grows the Cycle's tail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public void GrowTail(int numberOfSegments)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor tail = segments.Last<Actor>();
                Point velocity = tail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = tail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(color);
                segments.Add(segment);
            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment in segments)
            {
                segment.MoveNext();
            }

            for (int i = segments.Count - 1; i > 0; i--)
            {
                Actor trailing = segments[i];
                Actor previous = segments[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }

        /// <summary>
        /// Turns the head of the Cycle in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnHead(Point direction)
        {
            segments[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the Cycle body for moving.
        /// </summary>
        private void PrepareBody(int player)
        {
            //define body color
            color = player == 1 ? Constants.RED : Constants.YELLOW;

            int x = Constants.MAX_X / 2;
            int y = 0;

            if (player == 1)
            {
                y = Constants.MAX_Y / 4;
            }
            else if (player == 2)
            {
                y = Constants.MAX_Y / 4 * 3;
            }
            

            for (int i = 0; i < Constants.Cycle_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = "#";
                

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                segment.SetColor(color);
                segments.Add(segment);
            }
        }

        public void Kill()
        {
            color = Constants.WHITE;
            isAlive = false;
        }
    }
}