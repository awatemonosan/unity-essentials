using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate MemoType iListReduce<MemoType, ValueType>(MemoType memo, int index, ValueType value, List<ValueType> list);

public static class ListExtension {
  public static MemoType Reduce<ValueType, MemoType>(this List<ValueType> that, MemoType memo, iListReduce<MemoType, ValueType> reduceIterator) {
    for(int index = 0; index < that.Count; index ++) {
      memo = reduceIterator(memo, index, that[index], that);
    }
    return memo;
  }
  // public static T Sample<T>(this List<T> that){
  //   return that[Mathf.FloorToInt(Random.value * that.Count)];
  // }
}