﻿using Chess.Chessboard;
using Chess.Figures.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Bishop : Figure
    {
        private DiagonallyFigureMovment DiagonallyFigureMovment { get; } = new DiagonallyFigureMovment();
        public Bishop(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = new List<Field>();

            selectedFields = DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, -1));
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, 1));
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, -1));

            AttackedFields = selectedFields;
        }

        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = new List<Field>();

            selectedFields = DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, -1));
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, 1));
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, -1));

            return selectedFields
                            .Select(field => $"{field.Row - 1}{field.Col - 1}")
                            .ToHashSet();
        }
        
    }
}
