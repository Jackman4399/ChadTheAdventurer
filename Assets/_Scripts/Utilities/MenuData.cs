using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData {

    public MenuState menuState { get; private set; }
    public bool enabled;

    public MenuData(MenuState menuState) {
        this.menuState = menuState;
    }

}
