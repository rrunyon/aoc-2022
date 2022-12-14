import * as fs from 'fs';

function countOrderedPairs() {
  let input = fs.readFileSync('./13/input.txt', { encoding: 'utf8', flag: 'r' }).split("\n");
  let pairs = [];
  let currentPair = [];
  for (let line of input) {
    if (!line) {
      pairs.push(currentPair);
      currentPair = [];
    } else {
      currentPair.push(JSON.parse(line));
    }
  }
  pairs.push(currentPair);

  let inorderIndices = [];
  for (let i = 0; i < pairs.length; i++) {
    if (isOrderedPair(pairs[i])) inorderIndices.push(i + 1);
  }

  return inorderIndices.reduce((a, b) => a + b, 0);
}

function isOrderedPair(pair, index = 0) {
  let [left, right] = pair;
  if (index >= left.length && index >= right.length) {
    return undefined;
  } else if (index >= left.length) {
    return true;
  } else if (index >= right.length) {
    return false;
  }

  let leftVal = left[index];
  let rightVal = right[index];
  if (typeof leftVal === 'number' && typeof rightVal === 'number') {
    if (leftVal < rightVal) {
      return true;
    } else if (leftVal > rightVal) {
      return false;
    } else {
      return isOrderedPair(pair, index + 1, index + 1);
    }
  } else if (Array.isArray(leftVal) && Array.isArray(rightVal)) {
    let result = isOrderedPair([leftVal, rightVal], 0, 0);
    if (typeof result === 'boolean') {
      return result;
    } else {
      return isOrderedPair(pair, index + 1, index + 1);
    }
  } else if (typeof leftVal === 'number' && typeof rightVal !== 'number') {
    return isOrderedPair([[leftVal], rightVal], 0, 0);
  } else if (typeof leftVal !== 'number' && typeof rightVal === 'number') {
    return isOrderedPair([leftVal, [rightVal]], 0, 0);
  }

  return undefined;
}

console.log(countOrderedPairs());