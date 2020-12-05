use std::fs::File;
use std::io::prelude::*;
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

fn read_whole(fname: &str) -> String {
    let mut path: PathBuf = DATA.iter().collect();
    path.push(fname);
    let mut file = File::open(&path).unwrap();
    let mut string = String::new();
    file.read_to_string(&mut string).unwrap();
    string
}

pub fn day1() -> Vec<i64> {
    read_lines("day1.txt")
        .map(|x| x.unwrap().parse::<i64>().unwrap())
        .collect()
}

pub fn day2() -> Vec<String> {
    read_lines("day2.txt")
        .collect::<Result<Vec<_>, _>>()
        .unwrap()
}

pub fn day3() -> Vec<Vec<char>> {
    read_lines("day3.txt")
        .map(|line| line.unwrap().chars().collect::<Vec<_>>())
        .collect()
}

pub fn day4() -> Vec<String> {
    let string = read_whole("day4.txt");
    string.split("\n\n").map(str::to_owned).collect()
}

pub fn day5() -> Vec<String> {
    read_lines("day5.txt")
        .collect::<Result<Vec<_>, _>>()
        .unwrap()
}
