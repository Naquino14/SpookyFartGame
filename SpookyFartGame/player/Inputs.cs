using System                                                                        ;
using System.Collections.Generic                                                    ;
using System.Linq                                                                   ;
using System.Text                                                                   ;
using System.Threading.Tasks                                                        ;
using Microsoft.Xna.Framework                                                       ;
using Microsoft.Xna.Framework.Input                                                 ;

namespace SpookyFartGame.player{public enum Direction{
   center,up,down,left,right,upRight,upLeft,downRight,downLeft}
    
            public static class Inputs {
        public static Direction 
GetPlayerState(PlayerIndex index)
        {
            var kstate = Keyboard.GetState();
            bool up = kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up)        ,
            down = kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down)         ,
            left = kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left)         ,
            right = kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right)       ;

        if ((
up && down) 
                || (left && 
right))
            return Direction.center                                                 ;
                else if (up && right)
            return Direction.upRight                                                ;
                else if (up && left)
            return Direction.upLeft                                                 ;
            else if (down && right)
            return Direction.downRight                                              ;
            else if (down && left)
                return Direction.downLeft                                           ;
                else if (up)
            return Direction.up                                                     ;
             else if (down)
                return Direction.down                                               ;
            else if (left)  
                return Direction.left                                               ;
            else if (right)
           return Direction.right                                                   ;
            else
            return Direction.center                                                 ;
}}}
