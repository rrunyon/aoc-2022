import * as fs from 'fs';

function decodeSignal() {
  let input = fs.readFileSync('./13/input.txt', { encoding: 'utf8', flag: 'r' }).split("\n");
  let packets = [];
  for (let line of input) {
    if (line) {
      packets.push(JSON.parse(line));
    }
  }
  packets.push([[2]]);
  packets.push([[6]]);
  packets.sort(isOrderedPair);

  let decoderKey = 1;
  for (let i = 0; i < packets.length; i++) {
    let packet = JSON.stringify(packets[i]);
    if (packet === '[[2]]' || packet === '[[6]]') decoderKey *= (i + 1);
  }
  return decoderKey;
}

function isOrderedPair(left, right, index = 0) {
  if (index >= left.length && index >= right.length) {
    return undefined;
  } else if (index >= left.length) {
    return -1;
  } else if (index >= right.length) {
    return 1;
  }

  let leftVal = left[index];
  let rightVal = right[index];
  if (typeof leftVal === 'number' && typeof rightVal === 'number') {
    if (leftVal < rightVal) {
      return -1;
    } else if (leftVal > rightVal) {
      return 1;
    } else {
      return isOrderedPair(left, right, index + 1);
    }
  } else if (Array.isArray(leftVal) && Array.isArray(rightVal)) {
    let result = isOrderedPair(leftVal, rightVal, 0);
    if (typeof result === 'number') {
      return result;
    } else {
      return isOrderedPair(left, right, index + 1);
    }
  } else if (typeof leftVal === 'number' && typeof rightVal !== 'number') {
    return isOrderedPair([leftVal], rightVal, 0);
  } else if (typeof leftVal !== 'number' && typeof rightVal === 'number') {
    return isOrderedPair(leftVal, [rightVal], 0);
  }

  return undefined;
}

console.log(decodeSignal());