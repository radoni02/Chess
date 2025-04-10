﻿using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures;

public class King : Figure
{
    public King(bool isWhite, int value, string name) : base(isWhite, value, name)
    {
    }

    public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
    {
        Console.WriteLine("Mocked");
    }

    public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
    {

        var possibleKingMoves = new HashSet<string>();
        possibleKingMoves = KingPossibleMoves(checkerboard,currentField);
        return possibleKingMoves;

    }

    private HashSet<string> KingPossibleMoves(Checkerboard checkerboard, Field currentField)
    {
        var forbiddenFieldsForKing = checkerboard.Board.SelectMany(fl => fl)
                                                            .Where(f => f.IsUsed
                                                                    && f.Figure.IsWhite != currentField.Figure.IsWhite)
                                                            .Select(field => field.Figure)
                                                            .SelectMany(s => s.AttackedFields)
                                                            .Distinct();

        return checkerboard.Board.SelectMany(fl => fl)
                                    .Select(field =>
                                    {
                                        var lissss = new HashSet<Field>();
                                        if (PotentialFieldIsInKingMoveRange(field, currentField)
                                        && ((field.IsUsed && field.Figure.IsWhite != currentField.Figure.IsWhite) || !field.IsUsed))
                                        {
                                            lissss.Add(field);
                                        }
                                        return lissss;
                                    })
                                    .SelectMany(elf => elf)
                                    .Select(field =>
                                    {
                                        if(forbiddenFieldsForKing.Any(ff => ff.Row == field.Row && ff.Col == field.Col))
                                        {
                                            return string.Empty;
                                        }
                                        return $"{field.Row - 1}{field.Col - 1}";

                                    })
                                    .Where(value => !value.Equals(string.Empty))
                                    .ToHashSet();
    }


    private bool PotentialFieldIsInKingMoveRange(Field potentialField, Field currentField)
    {
        if ((potentialField.Row == currentField.Row
            || potentialField.Row == currentField.Row - 1
            || potentialField.Row == currentField.Row + 1)
            && (potentialField.Col == currentField.Col
            || potentialField.Col == currentField.Col - 1
            || potentialField.Col == currentField.Col + 1))
        {
            return true;
        }
        return false;
    }
}
