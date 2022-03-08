using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class AI
    {
        public static int DEPTH = 4;
        public static bool RUNNING = false;
        public static bool STOP = false;
        private static Player MAX = Player.BLACK;

        public static move_t MiniMaxAB(ChessBoard board, Player turn)
        {
            RUNNING = true; // we've started running
            STOP = false; // no interupt command sent
            MAX = turn; // who is maximizing

            //TBD: gather all possible moves;
            //storing best result from each round;
            //ForEach implements recursion logic;
            //decide the best move.           

         

            
        }

        private static int mimaab(ChessBoard board, Player turn, int depth, int alpha, int beta)
        {
            //TBD: implementation of alpha-beta pruning.
        }
    }
}
