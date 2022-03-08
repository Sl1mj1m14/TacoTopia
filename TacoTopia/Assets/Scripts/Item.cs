//Created by Keiler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item<T>
{

    private T element;
    private int amount;

    public Item() {
        element = default(T);
        amount = 0;
    }

    public Item(T type) {
        element = type;
        amount = 0;
    }

    public Item(T type, int num) {
        element = type;
        amount = num;
    }

    public void SetItem(T type) {
        element = type;
    }

    public void SetAmount(int num) {
        amount = num;
    }

    public void Set(T type) {
        Set(type,0);
    }

    public void Set(T type, int num) {
        SetItem(type);
        SetAmount(num);
    }

    public T GetItem() {
        return element;
    }

    public int GetAmount() {
        return amount;
    }

    public T Get() {
        return GetItem();
    }

    public void IncreaseAmount(int num) {
        amount += num;
    }

    public void IncreaseAmount() {
        amount++;
    }

    public void DecreaseAmount(int num) {
        amount -= num;
    }

    public void DecreaseAmount() {
        amount--;
    }

    public void ClearAmount() {
        amount = 0;
    }

    public bool Equals(T type){
        return element.Equals(type);
    }

    public override string ToString() {
        return element + "_" + amount;
    }

    
}
