module AOC.Day09

let onlySome data =
    data
    |> Seq.filter Option.isSome
    |> Seq.map (fun x -> x.Value)

let riskLevel height = height + 1

let getRowCol idx nx = (idx / nx, idx % nx)

let getNeighborIndices idx (ny, nx) =
    let (currentY, currentX) = getRowCol idx nx

    let left =
        if currentX - 1 >= 0 then
            Some(idx - 1)
        else
            None

    let right =
        if currentX + 1 < nx then
            Some(idx + 1)
        else
            None

    let up =
        if currentY + 1 < ny then
            Some(idx + nx)
        else
            None

    let down =
        if currentY - 1 >= 0 then
            Some(idx - nx)
        else
            None

    [ left; right; up; down ]

let getNeighborValues idx shape array =
    getNeighborIndices idx shape
    |> Seq.map (Option.map (Array.get array))

let arrayifyStringList (rows: string list) =
    let ny = List.length rows
    let nx = (List.head rows) |> String.length

    (rows
     |> Seq.fold
         (fun state row ->
             row
             |> Seq.map (int >> (fun x -> x - (int '0')))
             |> Seq.append state)
         Array.empty
     |> Array.ofSeq,
     (ny, nx))

let getNadirs array shape = 
    array
    |> Array.mapi (fun idx ele ->
        getNeighborValues idx shape array
        |> Seq.map (Option.filter ((>=) ele) >> Option.count)
        |> Seq.sum
        |> fun lowNeighbors ->
            if lowNeighbors > 0 then
                None
            else
                Some idx)
    |> onlySome

let part1 data =
    let (array, shape) = arrayifyStringList data

    getNadirs array shape
    |> Seq.map ((Array.get array) >> riskLevel)
    |> Seq.sum

let rec floodBasin (array: int []) shape basin toCheck =
    match toCheck with
    | [] -> basin
    | idx :: tail ->
        if Set.contains idx basin || array.[idx] = 9 then
            floodBasin array shape basin tail
        else
            floodBasin
                array
                shape
                (Set.add idx basin)
                (tail
                 @ (getNeighborIndices idx shape
                    |> onlySome
                    |> List.ofSeq))

let floodMap array shape nadirs =
    nadirs
    |> Seq.map (
        List.singleton
        >> (floodBasin array shape Set.empty)
    )

let part2 data =
    let (array, shape) = arrayifyStringList data

    getNadirs array shape
    |> floodMap array shape
    |> Seq.map Set.count
    |> Seq.sortDescending
    |> Seq.take 3
    |> Seq.fold (fun state ele -> ele * state) 1
