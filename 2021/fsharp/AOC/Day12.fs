module AOC.Day12

open System

let isSmallRoom name = Seq.head name |> Char.IsLower

let addNode builderMap (line: string) =
    let parts = line.Split('-')
    let first = parts.[0]
    let second = parts.[1]

    builderMap
    |> Map.change first (fun neighbors ->
        match neighbors with
        | Some n -> Some(second :: n)
        | None -> Some [ second ])
    |> Map.change second (fun neighbors ->
        match neighbors with
        | Some n -> Some(first :: n)
        | None -> Some [ first ])

let rec buildRoutes (cave: Map<string, string list>) room thisRoute =
    if room = "end" then
        [ room :: thisRoute ]
    elif isSmallRoom room
         && List.exists ((=) room) thisRoute then
        []
    else
        cave.[room]
        |> Seq.map (fun x -> buildRoutes cave x (room :: thisRoute))
        |> Seq.fold (fun state ele -> state @ ele) List.empty

let part1 data =
    let cave = data |> Seq.fold addNode Map.empty

    buildRoutes cave "start" List.empty
    |> Seq.filter (List.length >> ((<>) 0))
    |> Seq.length

let rec buildLongRoutes (cave: Map<string, string list>) visitedSmallTwice room thisRoute =
    if room = "end" then
        [ room :: thisRoute ]
    elif room = "start" && List.length thisRoute > 1 then
        []
    elif isSmallRoom room
         && List.exists ((=) room) thisRoute then
        if visitedSmallTwice then
            []
        else
            cave.[room]
            |> Seq.map (fun x -> buildLongRoutes cave true x (room :: thisRoute))
            |> Seq.fold (fun state ele -> state @ ele) List.empty
    else
        cave.[room]
        |> Seq.map (fun x -> buildLongRoutes cave visitedSmallTwice x (room :: thisRoute))
        |> Seq.fold (fun state ele -> state @ ele) List.empty

let part2 data =
    let cave = data |> Seq.fold addNode Map.empty

    buildLongRoutes cave false "start" List.empty
    |> Seq.filter (List.length >> ((<>) 0))
    |> Seq.length
