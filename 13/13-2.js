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
  packets.sort((a, b) => isOrderedPair([a, b]) ? -1 : 1);

  let decoderKey = 1;
  for (let i = 0; i < packets.length; i++) {
    let packet = JSON.stringify(packets[i]);
    if (packet === '[[2]]' || packet === '[[6]]') decoderKey *= (i + 1);
  }
  return decoderKey;
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

console.log(decodeSignal());