module AOC.Day05

open System
open System.Text.RegularExpressions

type Point = { X: int; Y: int }

module Point =
    let x point = point.X
    let y point = point.Y

type PartSwitcher =
    | Part1 of seq<Point>
    | Part2 of seq<Point>

module PartSwitcher =
    let isPart1 p =
        match p with
        | Part1 _ -> true
        | Part2 _ -> false

    let unwrap p =
        match p with
        | Part1 x -> x
        | Part2 x -> x

let produceStraightLines (start, finish) =
    let vector =
        (sign (finish.Y - start.Y), sign (finish.X - start.X))

    let outseq =
        Seq.unfold
            (fun location ->
                if location = finish then
                    None
                else
                    let next =
                        { Y = location.Y + fst vector
                          X = location.X + snd vector }
                    Some(location, next))
            start

    if start.Y = finish.Y || start.X = finish.X then
        Part1 (Seq.append outseq [finish])
    else
        Part2 (Seq.append outseq [finish])

let parseLine line =
    let m =
        Regex.Matches(line, @"(\d+),(\d+) -> (\d+),(\d+)")
        |> Seq.exactlyOne

    let groups =
        m.Groups
        |> Seq.map (fun g -> g.Value)
        |> Seq.skip 1
        |> Seq.map int
        |> Seq.toArray

    ({ X = groups[0]; Y = groups[1] }, { X = groups[2]; Y = groups[3] })

let updateOrDefault key updater fallback map =
    if Map.containsKey key map then
        Map.change key updater map
    else
        Map.add key fallback map

let part1 data =
    data
    |> Seq.map (parseLine >> produceStraightLines)
    |> Seq.filter PartSwitcher.isPart1
    |> Seq.map PartSwitcher.unwrap
    |> Seq.fold
        (fun map line ->
            Seq.fold (fun innerMap point -> updateOrDefault point (Option.map ((+) 1)) 1 innerMap) map line)
        Map.empty
    |> Map.fold (fun total _ ele -> if ele >= 2 then total + 1 else total) 0


let part2 data = 
    data
    |> Seq.map (parseLine >> produceStraightLines)
    |> Seq.map PartSwitcher.unwrap
    |> Seq.fold
        (fun map line ->
            Seq.fold (fun innerMap point -> updateOrDefault point (Option.map ((+) 1)) 1 innerMap) map line)
        Map.empty
    |> Map.fold (fun total _ ele -> if ele >= 2 then total + 1 else total) 0

