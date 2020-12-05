fn get_seat_id(item: &str) -> usize {
    item.chars().fold(0, |acc, elem| match elem {
        'R' => (acc << 1) + 1,
        'L' => acc << 1,
        'B' => (acc << 1) + 1,
        'F' => acc << 1,
        _ => panic!("Unknown letter {}", elem),
    })
}

fn part1(data: &[String]) -> usize {
    data.iter().map(|x| get_seat_id(x)).max().unwrap()
}

fn part2(data: &[String]) -> usize {
    let sorted = {
        let mut unsorted: Vec<_> = data.iter().map(|x| get_seat_id(x)).collect();
        unsorted.sort();
        unsorted
    };
    sorted[1..]
        .iter()
        .zip(sorted[..sorted.len() - 1].iter())
        .map(|(r, l)| r - l)
        .enumerate()
        .find(|(_, val)| *val == 2)
        .map(|(idx, _)| idx)
        .unwrap()
        + sorted[0]
        + 1
}

#[cfg(test)]
mod tests {

    #[test]
    fn day5_part1() {
        let data = [
            "FBFBBFFRLR".to_string(),
            "BFFFBBFRRR".to_string(),
            "FFFBBBFRRR".to_string(),
            "BBFFBBFRLL".to_string(),
        ];
        let truths = [357, 567, 119, 820];
        for (item, truth) in data.iter().zip(&truths) {
            assert_eq!(*truth, crate::day5::get_seat_id(item));
        }
        assert_eq!(820, crate::day5::part1(&data));
        assert_eq!(880, crate::day5::part1(&crate::parsers::day5()));
    }

    #[test]
    fn day5_part2() {
        assert_eq!(731, crate::day5::part2(&crate::parsers::day5()));
    }
}
