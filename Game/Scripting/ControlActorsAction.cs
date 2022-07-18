using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the Cycle.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the Cycle's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService keyboardService;
        private Point direction1 = new Point(Constants.CELL_SIZE, 0);
        private Point direction2 = new Point(Constants.CELL_SIZE, 0);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this.keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            // left
            if (keyboardService.IsKeyDown("a"))
            {
                direction1 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (keyboardService.IsKeyDown("d"))
            {
                direction1 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (keyboardService.IsKeyDown("w"))
            {
                direction1 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (keyboardService.IsKeyDown("s"))
            {
                direction1 = new Point(0, Constants.CELL_SIZE);
            }

            Cycle Cycle1 = (Cycle)cast.GetFirstActor("Cycle1");
            Cycle1.TurnHead(direction1);

            // left
            if (keyboardService.IsKeyDown("j"))
            {
                direction2 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (keyboardService.IsKeyDown("l"))
            {
                direction2 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (keyboardService.IsKeyDown("i"))
            {
                direction2 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (keyboardService.IsKeyDown("k"))
            {
                direction2 = new Point(0, Constants.CELL_SIZE);
            }

            Cycle Cycle2 = (Cycle)cast.GetFirstActor("Cycle2");
            Cycle2.TurnHead(direction2);
            Cycle1.GrowTail(1);
            Cycle2.GrowTail(1);

        }


    }
}