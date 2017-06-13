﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate MemoType ListReducer<MemoType, ValueType>(MemoType memo, int index, ValueType value, List<ValueType> list);
public delegate ValueType ListMapper<ValueType>(int index, ValueType value, List<ValueType> list);

public static class ListExtension {
  public static string Join <ValueType>(this List<ValueType> that, string separator="")
  {
    return that.Reduce <ValueType, string> (
      delegate(string memo, int index, ValueType value, List<ValueType> list)
      {
        memo += (memo==""?"":separator) + value.ToString();
        return memo;
      }, ""
    );
  }

  public static void Push<ValueType>(this List<ValueType> that, ValueType value)
  {
    that.Add(value);
  }

  public static ValueType Pop <ValueType>(this List<ValueType> that)
  {
    return that.PopAt<ValueType>(that.Count-1);
  }

  public static ValueType Shift <ValueType>(this List<ValueType> that)
  {
    return that.PopAt<ValueType>(0);
  }

  public static ValueType PopAt <ValueType> (this List<ValueType> that, int index)
  {
    ValueType r = that[index];
    that.RemoveAt(index);
    return r;
  }

  public static MemoType Reduce<ValueType, MemoType> (
    this List<ValueType> that,
    ListReducer<MemoType,ValueType> reduceIterator,
    MemoType memo
  )
  {
    for(int index = 0; index < that.Count; index ++)
    {
      memo = reduceIterator(memo, index, that[index], that);
    }
    return memo;
  }

  public static List<ValueType> Map<ValueType> (
    this List<ValueType> that,
    ListMapper<ValueType> mapIterator
  )
  {
    List<ValueType> mapResult = new List<ValueType>();
    for(int index = 0; index < that.Count; index ++)
    {
      mapResult.Push(mapIterator(index, that[index], that));
    }
    return mapResult;
  }

  // public static ValueType Sample<ValueType>(
  //   this List<ValueType> that
  // )
  // {
  //   return that[Mathf.FloorToInt(Random.value * that.Count)];
  // }
}