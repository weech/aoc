module AOC.Day11

let getRowCol idx nx = (idx / nx, idx % nx)
let getLinear nx (row, col) = row * nx + col

let checkValidIndex ny nx (testY, testX) =
    0 <= testY
    && testY < ny
    && 0 <= testX
    && testX < nx

let getNeighborIndices idx (ny, nx) =
    let neighbors =
        [| (0, 1)
           (0, -1)
           (1, 0)
           (-1, 0)
           (1, 1)
           (-1, 1)
           (1, -1)
           (-1, -1) |]

    let (currentY, currentX) = getRowCol idx nx

    neighbors
    |> Seq.map (fun (offY, offX) -> (offY + currentY, offX + currentX))
    |> Seq.filter (checkValidIndex ny nx)
    |> Seq.map (getLinear nx)

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

let increaseAll octos = octos |> Array.map ((+) 1)

let increaseSome octos indices =
    octos
    |> Array.mapi (fun idx v ->
        if Seq.exists ((=) idx) indices then
            v + 1
        else
            v)

let rec flash octos shape flashers flashed =
    match flashers with
    | [] -> (octos, flashed)
    | head :: tail ->
        if Set.contains head flashed then
            // Already flashed this one because more than one of the neighbors flashed
            flash octos shape tail flashed
        else
            let neighbors = getNeighborIndices head shape
            let newMap = increaseSome octos neighbors

            let newFlashers =
                neighbors
                |> Seq.filter (fun idx -> newMap.[idx] > 9)
                |> List.ofSeq

            flash newMap shape (tail @ newFlashers) (Set.add head flashed)

let resetEnergy octos =
    Array.map (fun v -> if v > 9 then 0 else v) octos

let takeStep octos shape =
    let newMap = increaseAll octos

    let flashers =
        newMap
        |> Seq.mapi (fun idx ele -> (idx, ele))
        |> Seq.filter (snd >> ((<) 9))
        |> Seq.map fst
        |> List.ofSeq

    let (flashingMap, flashed) = flash newMap shape flashers Set.empty
    (resetEnergy flashingMap, Set.count flashed)

let rec drivingLoop octos shape count step =
    if step >= 100 then
        count
    else
        let (newMap, increase) = takeStep octos shape
        drivingLoop newMap shape (count + increase) (step + 1)

let part1 data =
    let (octos, shape) = arrayifyStringList data
    drivingLoop octos shape 0 0

let rec drivingLoop2 octos shape count step =
    let (newMap, increase) = takeStep octos shape

    if increase = 100 then
        step + 1
    else
        drivingLoop2 newMap shape (count + increase) (step + 1)

let part2 data =
    let (octos, shape) = arrayifyStringList data
    drivingLoop2 octos shape 0 0
