use std::fs::File;
use std::io::{BufRead, BufReader};
use std::path::PathBuf;

const DATA: [&str; 3] = ["..", "..", "data"];

fn read_lines(fname: &str) -> std::io::Lines<BufReader<File>> {
    let mut path: PathBuf = DATA.iter().collect();
    path.push(fname);
    let file = File::open(&path).unwrap();
    let reader = BufReader::new(file);
    reader.lines()
}

pub fn day1() -> Vec<i64> {
    read_lines("day1.txt")
        .map(|x| x.unwrap().parse::<i64>().unwrap())
        .collect()
}

pub fn day2() -> Vec<String> {
    read_lines("day1.txt")
        .collect::<Result<Vec<_>, _>>()
        .unwrap()
}
