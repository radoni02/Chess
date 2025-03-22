﻿using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Pawn : Figure
    {
        public Pawn(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard,Field currentField)
        {

            if(currentField.Figure.IsWhite)
            {
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, -2, 0);
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, 0, 0);
            }
            
            if(!currentField.Figure.IsWhite)
            {
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, -2, -2);
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, 0, -2);
            }
        }


        private void CheckIfShloudAddToAttackedFields(Checkerboard checkerboard, Field currentField,int adjustValueCol, int adjustValueRow)
        {
            if (CheckIfFieldIsOutOfTheBoard(checkerboard, currentField.Row + adjustValueRow, currentField.Col + adjustValueCol))
                return;
            if (IsWhite && !checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].IsUsed)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol]);
            }
            if(IsWhite &&
                checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].IsUsed &&
                !checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].Figure.IsWhite)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol]);
            }

            if(!IsWhite && !checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col+ adjustValueCol].IsUsed)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol]);
            }
            if (!IsWhite &&
                checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol].IsUsed &&
                checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol].Figure.IsWhite)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol]);
            }
        }


        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var additionalPosiibleMoves = currentField.Figure.AttackedFields.Where(f => f.IsUsed)
                .Select(filed =>
                {
                    return $"{filed.Row - 1}{filed.Col - 1}";
                })
                .ToHashSet();
            if (currentField.Figure.IsWhite)
            {
                var possibleWhiteMoves = new HashSet<string>();
                var forwardMove = ForwardMoveWhite(checkerboard, currentField);
                if (forwardMove is not "")
                {
                    possibleWhiteMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoWhite(checkerboard, currentField);
                if (moveByTwo is not "")
                {
                    possibleWhiteMoves.Add(moveByTwo);
                }
                return possibleWhiteMoves.Union(additionalPosiibleMoves)
                    .ToHashSet();
                
            }

            if(!currentField.Figure.IsWhite)
            {
                var possibleBlackMoves = new HashSet<string>();
                var forwardMove = ForwardMoveBlack(checkerboard, currentField);
                if (forwardMove is not "")
                {
                    possibleBlackMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoBlack(checkerboard, currentField);
                if (moveByTwo is not "")
                {
                    possibleBlackMoves.Add(moveByTwo);
                }
                return possibleBlackMoves.Union(additionalPosiibleMoves)
                    .ToHashSet();
            }

            return new HashSet<string>();
        }

        private string ForwardMoveByTwoWhite(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row + 1, currentField.Col);
            if ((currentField.Row == 7 || currentField.Row == 2) &&
                ForwardMoveWhite(checkerboard, tempField) is string moveResult && 
                moveResult is not "")
            {
                return moveResult;
            }
            return "";
        }

        private string ForwardMoveByTwoBlack(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row - 1, currentField.Col);
            if ((currentField.Row == 7 || currentField.Row == 2) &&
                ForwardMoveBlack(checkerboard, tempField) is string moveResult &&
                moveResult is not "")
            {
                return moveResult;
            }
            return "";
        }


        private string ForwardMoveWhite(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField,0);
        }

        private string ForwardMoveBlack(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField, -2);
        }

        private string ForwardMove(Checkerboard checkerboard, Field currentField,int moveByValue)
        {
            return !checkerboard.Board[currentField.Row + moveByValue][currentField.Col - 1].IsUsed ? $"{currentField.Row + moveByValue}{currentField.Col - 1}" : string.Empty;
        }
    }
}
