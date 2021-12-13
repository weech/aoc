module AOC.Day13

open System.Text.RegularExpressions
open System

type Fold =
    | Y of int
    | X of int

let makeFold (line: string) =
    let m =
        Regex.Matches(line, @"fold along (x|y)=(\d+)")
        |> Seq.exactlyOne

    let groups =
        m.Groups
        |> Seq.map (fun g -> g.Value)
        |> Seq.skip 1
        |> Seq.toArray

    if groups.[0] = "x" then
        X(int groups.[1])
    else
        Y(int groups.[1])

let getPostFoldLocation fold (currentX, currentY) =
    match fold with
    | Y value ->
        if value < currentY then
            (currentX, (value - (currentY - value)))
        else
            (currentX, currentY)
    | X value ->
        if value < currentX then
            ((value - (currentX - value)), currentY)
        else
            (currentX, currentY)

let fold map foldInfo =
    Set.map (getPostFoldLocation foldInfo) map

let makeLocation (line: string) =
    let coords = line.Split(',') |> Array.map int
    (coords.[0], coords.[1])

let part1 paper folds =
    let map =
        paper |> Seq.map makeLocation |> Set.ofSeq

    let foldList = folds |> Seq.map makeFold

    fold map (Seq.head foldList) |> Set.count

let render map =
    let nx =
        map |> Set.toSeq |> Seq.map fst |> Seq.max

    let ny =
        map |> Set.toSeq |> Seq.map snd |> Seq.max

    seq { 0 .. ny }
    |> Seq.map (fun y ->
        let row =
            map |> Set.filter (snd >> (=) y) |> Set.map fst

        seq { 0 .. nx }
        |> Seq.map (fun x -> if Set.contains x row then '█' else ' ')
        |> fun this -> Seq.append this (Seq.singleton '\n'))
    |> Seq.concat
    |> String.Concat

let part2 paper folds =
    let startingMap =
        paper |> Seq.map makeLocation |> Set.ofSeq

    folds
    |> Seq.map makeFold
    |> Seq.fold (fun state ele -> fold state ele) startingMap
    |> render
