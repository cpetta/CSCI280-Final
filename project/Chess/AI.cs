using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class AI
    {
        public static int DEPTH = 1;
        public static bool RUNNING = false;
        public static bool STOP = false;
        private static Player MAX = Player.BLACK;
        private static Player MIN = MAX == Player.BLACK ? Player.WHITE : Player.BLACK;
        private static Queue<move_t> PreviousMoves = new Queue<move_t>();

        public static Tree MiniMax(Tree t, Player player, int depth)
        {

            // If the game is over either because there's a winner or there are no more empty spaces.
            if (depth == 0)
            {
                t.Fitness = t.Board.fitness(player);
                return t;
            }
            
            depth--;

            if (player == MAX)
            {
                t.Fitness = int.MinValue;
                foreach (Tree child in t.children)
                {
                    t.Fitness = Math.Max(t.Fitness, MiniMax(child, MIN, depth).Fitness);
                }
            }
            else
            {
                t.Fitness = int.MaxValue;
                foreach (Tree child in t.children)
                {
                    t.Fitness = Math.Min(t.Fitness, MiniMax(child, MAX, depth).Fitness);
                }
            }
            return t;
        }

        public static move_t MiniMaxAB(ChessBoard board, Player turn)
        {
            RUNNING = true; // we've started running
            STOP = false; // no interupt command sent
            MAX = turn; // who is maximizing

            Tree root = new Tree(board, turn);
            root = BuildTree(root, turn, DEPTH);
            Tree moveTree = MiniMax(root, turn, DEPTH);
            move_t bestMove = new move_t(new position_t(-1, -1), new position_t(-1, -1));
            foreach (Tree child in moveTree)
            {
                if (!PreviousMoves.Contains(child.move))
                    if (moveTree.Fitness <= child.Fitness)
                        bestMove = child.move;
            }
            if (PreviousMoves.Count > 10)
                PreviousMoves.Dequeue();
            PreviousMoves.Enqueue(bestMove);
            return bestMove;
            //TBD: gather all possible moves;
            //storing best result from each round;
            //ForEach implements recursion logic;
            //decide the best move.           

         

            
        }

        //private static int mimaab(ChessBoard board, Player turn, int depth, int alpha, int beta)
        //{
            //TBD: implementation of alpha-beta pruning.
        //}
        private static Tree BuildTree(Tree chessBoard, Player player, int depth)
        {
            if (depth > 0)
            {
                List<Tree> children = getNextChessBoards(chessBoard, player);
                foreach (Tree child in children)
                {
                    chessBoard.addChild(child);
                    BuildTree(child, child.PlayerTurn, depth - 1);
                }
            }
            return chessBoard;
        }

        private static List<Tree> getNextChessBoards(Tree board, Player player)
        {
            Player otherPlayer = player == Player.BLACK ? Player.WHITE : Player.BLACK;
            List<Tree> chessBoards = new List<Tree>();
            List<move_t> moves = LegalMoveSet.GetMoves(board.Board, player);
            foreach (move_t move in moves)
            {
                ChessBoard newBoard = LegalMoveSet.move(board.Board, move);
                Tree newTree = new Tree(newBoard, otherPlayer);
                newTree.move = move;
                chessBoards.Add(newTree);
            }
            return chessBoards;
        }

    }
}
