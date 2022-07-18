using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
   /// <summary>
   /// <para>An update action that handles interactions between the actors.</para>
   /// <para>
   /// The responsibility of HandleCollisionsAction is to handle the situation when the Cycle 
   /// collides with the food, or the Cycle collides with its segments, or the game is over.
   /// </para>
   /// </summary>
   public class HandleCollisionsAction : Action
   {
      private bool isGameOver = false;

      /// <summary>
      /// Constructs a new instance of HandleCollisionsAction.
      /// </summary>
      public HandleCollisionsAction()
      {
      }

      /// <inheritdoc/>
      public void Execute(Cast cast, Script script)
      {
         if (isGameOver == false)
         {
            // HandleFoodCollisions(cast);
            HandleSegmentCollisions(cast);
            HandleGameOver(cast);
         }
      }

      /// <summary>
      /// Updates the score nd moves the food if the Cycle collides with it.
      /// </summary>
      /// <param name="cast">The cast of actors.</param>
      // private void HandleFoodCollisions(Cast cast)
      // {
      //     Cycle Cycle = (Cycle)cast.GetFirstActor("Cycle");
      //     Score score = (Score)cast.GetFirstActor("score");
      //     Food food = (Food)cast.GetFirstActor("food");

      //     if (Cycle.GetHead().GetPosition().Equals(food.GetPosition()))
      //     {
      //         int points = food.GetPoints();
      //         Cycle.GrowTail(points);
      //         score.AddPoints(points);
      //         food.Reset();
      //     }
      // }

      /// <summary>
      /// Sets the game over flag if the Cycle collides with one of its segments.
      /// </summary>
      /// <param name="cast">The cast of actors.</param>
      private void HandleSegmentCollisions(Cast cast)
      {
         Cycle Cycle1 = (Cycle)cast.GetFirstActor("Cycle1");
         Cycle Cycle2 = (Cycle)cast.GetFirstActor("Cycle2");
         Actor head1 = Cycle1.GetHead();
         Actor head2 = Cycle2.GetHead();
         List<Actor> body = Cycle1.GetBody();
         body.AddRange(Cycle2.GetBody());

         foreach (Actor segment in body)
         {
            if (segment.GetPosition().Equals(head1.GetPosition()) ||
                segment.GetPosition().Equals(head2.GetPosition()) ||
                head1.GetPosition().Equals(head2.GetPosition()))
            {
               isGameOver = true;
            }
         }
      }

      private void HandleGameOver(Cast cast)
      {
         if (isGameOver == true)
         {
            Cycle Cycle1 = (Cycle)cast.GetFirstActor("Cycle1");
            Cycle Cycle2 = (Cycle)cast.GetFirstActor("Cycle2");
            Cycle1.Kill();
            Cycle2.Kill();
            List<Actor> segments = Cycle1.GetSegments();
            segments.AddRange(Cycle2.GetSegments());
            Food food = (Food)cast.GetFirstActor("food");

            // create a "game over" message
            int x = Constants.MAX_X / 2;
            int y = Constants.MAX_Y / 2;
            Point position = new Point(x, y);

            Actor message = new Actor();
            message.SetText("Game Over!");
            message.SetPosition(position);
            cast.AddActor("messages", message);

            // make everything white
            foreach (Actor segment in segments)
            {
               segment.SetColor(Constants.WHITE);
            }
            food.SetColor(Constants.WHITE);
         }
      }

   }
}