import * as fs from 'fs';

function solution() {
  const input = fs.readFileSync('./22/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  const { board, instructions } = parseInput(input);

  // right, down, left, up
  const directions = [[0, 1], [1, 0], [0, -1], [-1, 0]];

  // which direction we're facing
  let heading = 0

  let instructionsPointer = 0;

  let currentPosition = getInitialPosition(board);

  while (instructionsPointer < instructions.length) {
    let number = '';
    while (!isNaN(parseInt(instructions[instructionsPointer]))) {
      number += instructions[instructionsPointer++];
    }
    number = parseInt(number);
    console.log("number: ", number);
    print(currentPosition, board);

    for (let i = 0; i < number; i++) {
      console.log('-----------------------');
      let [y, x] = currentPosition;
      let direction = directions[heading];
      let nextY = y + direction[0];
      let nextX = x + direction[1];
      let nextPosition = [nextY, nextX];

      if (isOutOfBounds(nextPosition, board)) {
        console.log('out of bounds');
        if (heading === 0) {
          let tryX = 0;
          while (board[y][tryX] === ' ') {
            tryX++;
          }

          if (board[y][tryX] === '#') {
            break;
          } else {
            nextX = tryX;
          }
        } else if (heading === 1) {
          let tryY = 0;
          while (board[tryY][x] === ' ') {
            tryY++;
          }

          if (board[tryY][x] === '#') {
            break;
          } else {
            nextY = tryY;
          }
        } else if (heading === 2) {
          let tryX = board[0].length - 1;
          while (board[y][tryX] === ' ') {
            tryX--;
          }

          if (board[y][tryX] === '#') {
            break;
          } else {
            nextX = tryX;
          }
        } else if (heading === 3) {
          let tryY = board.length - 1;
          while (board[tryY][x] === ' ') {
            tryY--;
          }

          if (board[tryY][x] === '#') {
            break;
          } else {
            nextY = tryY;
          }
        }
      } else if (board[nextY][nextX] === '#') {
        console.log('blocked');
        break;
      }

      let headingChar = ['>', 'v', '<', '^'];
      board[y][x] = headingChar[heading];
      currentPosition = [nextY, nextX];
      print(currentPosition, board);
    }

    let turn = instructions[instructionsPointer++];
    if (turn) {
      heading = turn === 'R' ? heading + 1 : heading - 1;
      heading = (heading + 4) % 4;
    }
  }

  console.log(currentPosition, heading);
  return (1000 * (currentPosition[0] + 1)) + (4 * (currentPosition[1] + 1)) + heading;
}

function print([y, x], board) {
  console.log();
  for (let i = 0; i < board.length; i++) {
    let row = [];
    for (let j = 0; j < board.length; j++) {
      if (i === y && j === x) {
        row.push('X');
      } else {
        row.push(board[i][j]);
      }
    }
    console.log(row.join(''));
  }
}

function parseInput(input) {
  const board = [];
  let i = 0;
  while (input[i]) {
    board.push(input[i].split(''));
    i++;
  }

  const instructions = input[input.length - 2];

  return { board, instructions };
}

function getInitialPosition(board) {
  for (let i = 0; i < board.length; i++) {
    for (let j = 0; j < board.length; j++) {
      if (board[i][j] === '.') return [i, j];
    }
  }
}

function isOutOfBounds(position, board) {
  let [y, x] = position;

  if (x < 0 || x >= board[0].length || y < 0 || y >= board.length) return true;
  if (board[y][x] === ' ') return true;

  return false;
}

console.log(solution());
