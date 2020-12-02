#![allow(dead_code)]

mod parsers;

fn day1_part1(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for right in left..data.len() {
            if data[left] + data[right] == 2020 {
                return data[left] * data[right];
            }
        }
    }
    0
}

fn day1_part2(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for center in left..data.len() {
            for right in center..data.len() {
                if data[left] + data[right] + data[center] == 2020 {
                    return data[left] * data[right] * data[center];
                }
            }
        }
    }
    0
}

#[cfg(test)]
mod tests {
    #[test]
    fn day1_part1() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(crate::day1_part1(&testdata), 514579);
        assert_eq!(crate::day1_part1(&crate::parsers::day1()), 842016);
    }

    #[test]
    fn day1_part2() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(crate::day1_part2(&testdata), 241861950);
        assert_eq!(crate::day1_part2(&crate::parsers::day1()), 9199664);
    }
}
