using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

public class UInput : Singleton<UInput>
{
  private static Dictionary<string, InputWrapper> inputName2Input = new Dictionary<string, InputWrapper>( );
  private static List<InputWrapper> inputs = new List<InputWrapper>( );

  public static InputWrapper AddInput(InputWrapper input)
  {
    inputs.Add(input);
    inputName2Input[input.inputName] = input;
    return input;
  }

  static public InputWrapper Get(string inputName)
  {
    return inputName2Input[inputName];
  }

  static public BindInput CreateBind(string bindName, string catagory, bool isBinary)
  {
    return (BindInput)AddInput(new BindInput(bindName, catagory, isBinary));
  }
  void Start() {
    InitKeyboard();
    InitJoystick();
    InitMouse();
  }

  void Update() {
    UpdateInputs();
  }

  private void InitJoystick() {
    UInput.AddInput(new JoystickButtonInput(0, 0, KeyCode.Joystick1Button0));
    UInput.AddInput(new JoystickButtonInput(0, 1,  KeyCode.Joystick1Button1));
    UInput.AddInput(new JoystickButtonInput(0, 2,  KeyCode.Joystick1Button2));
    UInput.AddInput(new JoystickButtonInput(0, 3,  KeyCode.Joystick1Button3));
    UInput.AddInput(new JoystickButtonInput(0, 4,  KeyCode.Joystick1Button4));
    UInput.AddInput(new JoystickButtonInput(0, 5,  KeyCode.Joystick1Button5));
    UInput.AddInput(new JoystickButtonInput(0, 6,  KeyCode.Joystick1Button6));
    UInput.AddInput(new JoystickButtonInput(0, 7,  KeyCode.Joystick1Button7));
    UInput.AddInput(new JoystickButtonInput(0, 8,  KeyCode.Joystick1Button8));
    UInput.AddInput(new JoystickButtonInput(0, 9,  KeyCode.Joystick1Button9));
    UInput.AddInput(new JoystickButtonInput(0, 10, KeyCode.Joystick1Button10));
    UInput.AddInput(new JoystickButtonInput(0, 11, KeyCode.Joystick1Button11));
    UInput.AddInput(new JoystickButtonInput(0, 12, KeyCode.Joystick1Button12));
    UInput.AddInput(new JoystickButtonInput(0, 13, KeyCode.Joystick1Button13));
    UInput.AddInput(new JoystickButtonInput(0, 14, KeyCode.Joystick1Button14));
    UInput.AddInput(new JoystickButtonInput(0, 15, KeyCode.Joystick1Button15));
    UInput.AddInput(new JoystickButtonInput(0, 16, KeyCode.Joystick1Button16));
    UInput.AddInput(new JoystickButtonInput(0, 17, KeyCode.Joystick1Button17));
    UInput.AddInput(new JoystickButtonInput(0, 18, KeyCode.Joystick1Button18));
    UInput.AddInput(new JoystickButtonInput(0, 19, KeyCode.Joystick1Button19));

    UInput.AddInput(new JoystickButtonInput(1, 0,  KeyCode.Joystick2Button0));
    UInput.AddInput(new JoystickButtonInput(1, 1,  KeyCode.Joystick2Button1));
    UInput.AddInput(new JoystickButtonInput(1, 2,  KeyCode.Joystick2Button2));
    UInput.AddInput(new JoystickButtonInput(1, 3,  KeyCode.Joystick2Button3));
    UInput.AddInput(new JoystickButtonInput(1, 4,  KeyCode.Joystick2Button4));
    UInput.AddInput(new JoystickButtonInput(1, 5,  KeyCode.Joystick2Button5));
    UInput.AddInput(new JoystickButtonInput(1, 6,  KeyCode.Joystick2Button6));
    UInput.AddInput(new JoystickButtonInput(1, 7,  KeyCode.Joystick2Button7));
    UInput.AddInput(new JoystickButtonInput(1, 8,  KeyCode.Joystick2Button8));
    UInput.AddInput(new JoystickButtonInput(1, 9,  KeyCode.Joystick2Button9));
    UInput.AddInput(new JoystickButtonInput(1, 10, KeyCode.Joystick2Button10));
    UInput.AddInput(new JoystickButtonInput(1, 11, KeyCode.Joystick2Button11));
    UInput.AddInput(new JoystickButtonInput(1, 12, KeyCode.Joystick2Button12));
    UInput.AddInput(new JoystickButtonInput(1, 13, KeyCode.Joystick2Button13));
    UInput.AddInput(new JoystickButtonInput(1, 14, KeyCode.Joystick2Button14));
    UInput.AddInput(new JoystickButtonInput(1, 15, KeyCode.Joystick2Button15));
    UInput.AddInput(new JoystickButtonInput(1, 16, KeyCode.Joystick2Button16));
    UInput.AddInput(new JoystickButtonInput(1, 17, KeyCode.Joystick2Button17));
    UInput.AddInput(new JoystickButtonInput(1, 18, KeyCode.Joystick2Button18));
    UInput.AddInput(new JoystickButtonInput(1, 19, KeyCode.Joystick2Button19));

    UInput.AddInput(new JoystickButtonInput(2, 0,  KeyCode.Joystick3Button0));
    UInput.AddInput(new JoystickButtonInput(2, 1,  KeyCode.Joystick3Button1));
    UInput.AddInput(new JoystickButtonInput(2, 2,  KeyCode.Joystick3Button2));
    UInput.AddInput(new JoystickButtonInput(2, 3,  KeyCode.Joystick3Button3));
    UInput.AddInput(new JoystickButtonInput(2, 4,  KeyCode.Joystick3Button4));
    UInput.AddInput(new JoystickButtonInput(2, 5,  KeyCode.Joystick3Button5));
    UInput.AddInput(new JoystickButtonInput(2, 6,  KeyCode.Joystick3Button6));
    UInput.AddInput(new JoystickButtonInput(2, 7,  KeyCode.Joystick3Button7));
    UInput.AddInput(new JoystickButtonInput(2, 8,  KeyCode.Joystick3Button8));
    UInput.AddInput(new JoystickButtonInput(2, 9,  KeyCode.Joystick3Button9));
    UInput.AddInput(new JoystickButtonInput(2, 10, KeyCode.Joystick3Button10));
    UInput.AddInput(new JoystickButtonInput(2, 11, KeyCode.Joystick3Button11));
    UInput.AddInput(new JoystickButtonInput(2, 12, KeyCode.Joystick3Button12));
    UInput.AddInput(new JoystickButtonInput(2, 13, KeyCode.Joystick3Button13));
    UInput.AddInput(new JoystickButtonInput(2, 14, KeyCode.Joystick3Button14));
    UInput.AddInput(new JoystickButtonInput(2, 15, KeyCode.Joystick3Button15));
    UInput.AddInput(new JoystickButtonInput(2, 16, KeyCode.Joystick3Button16));
    UInput.AddInput(new JoystickButtonInput(2, 17, KeyCode.Joystick3Button17));
    UInput.AddInput(new JoystickButtonInput(2, 18, KeyCode.Joystick3Button18));
    UInput.AddInput(new JoystickButtonInput(2, 19, KeyCode.Joystick3Button19));

    UInput.AddInput(new JoystickButtonInput(3, 0,  KeyCode.Joystick4Button0));
    UInput.AddInput(new JoystickButtonInput(3, 1,  KeyCode.Joystick4Button1));
    UInput.AddInput(new JoystickButtonInput(3, 2,  KeyCode.Joystick4Button2));
    UInput.AddInput(new JoystickButtonInput(3, 3,  KeyCode.Joystick4Button3));
    UInput.AddInput(new JoystickButtonInput(3, 4,  KeyCode.Joystick4Button4));
    UInput.AddInput(new JoystickButtonInput(3, 5,  KeyCode.Joystick4Button5));
    UInput.AddInput(new JoystickButtonInput(3, 6,  KeyCode.Joystick4Button6));
    UInput.AddInput(new JoystickButtonInput(3, 7,  KeyCode.Joystick4Button7));
    UInput.AddInput(new JoystickButtonInput(3, 8,  KeyCode.Joystick4Button8));
    UInput.AddInput(new JoystickButtonInput(3, 9,  KeyCode.Joystick4Button9));
    UInput.AddInput(new JoystickButtonInput(3, 10, KeyCode.Joystick4Button10));
    UInput.AddInput(new JoystickButtonInput(3, 11, KeyCode.Joystick4Button11));
    UInput.AddInput(new JoystickButtonInput(3, 12, KeyCode.Joystick4Button12));
    UInput.AddInput(new JoystickButtonInput(3, 13, KeyCode.Joystick4Button13));
    UInput.AddInput(new JoystickButtonInput(3, 14, KeyCode.Joystick4Button14));
    UInput.AddInput(new JoystickButtonInput(3, 15, KeyCode.Joystick4Button15));
    UInput.AddInput(new JoystickButtonInput(3, 16, KeyCode.Joystick4Button16));
    UInput.AddInput(new JoystickButtonInput(3, 17, KeyCode.Joystick4Button17));
    UInput.AddInput(new JoystickButtonInput(3, 18, KeyCode.Joystick4Button18));
    UInput.AddInput(new JoystickButtonInput(3, 19, KeyCode.Joystick4Button19));

    UInput.AddInput(new JoystickButtonInput(4, 0,  KeyCode.Joystick5Button0));
    UInput.AddInput(new JoystickButtonInput(4, 1,  KeyCode.Joystick5Button1));
    UInput.AddInput(new JoystickButtonInput(4, 2,  KeyCode.Joystick5Button2));
    UInput.AddInput(new JoystickButtonInput(4, 3,  KeyCode.Joystick5Button3));
    UInput.AddInput(new JoystickButtonInput(4, 4,  KeyCode.Joystick5Button4));
    UInput.AddInput(new JoystickButtonInput(4, 5,  KeyCode.Joystick5Button5));
    UInput.AddInput(new JoystickButtonInput(4, 6,  KeyCode.Joystick5Button6));
    UInput.AddInput(new JoystickButtonInput(4, 7,  KeyCode.Joystick5Button7));
    UInput.AddInput(new JoystickButtonInput(4, 8,  KeyCode.Joystick5Button8));
    UInput.AddInput(new JoystickButtonInput(4, 9,  KeyCode.Joystick5Button9));
    UInput.AddInput(new JoystickButtonInput(4, 10, KeyCode.Joystick5Button10));
    UInput.AddInput(new JoystickButtonInput(4, 11, KeyCode.Joystick5Button11));
    UInput.AddInput(new JoystickButtonInput(4, 12, KeyCode.Joystick5Button12));
    UInput.AddInput(new JoystickButtonInput(4, 13, KeyCode.Joystick5Button13));
    UInput.AddInput(new JoystickButtonInput(4, 14, KeyCode.Joystick5Button14));
    UInput.AddInput(new JoystickButtonInput(4, 15, KeyCode.Joystick5Button15));
    UInput.AddInput(new JoystickButtonInput(4, 16, KeyCode.Joystick5Button16));
    UInput.AddInput(new JoystickButtonInput(4, 17, KeyCode.Joystick5Button17));
    UInput.AddInput(new JoystickButtonInput(4, 18, KeyCode.Joystick5Button18));
    UInput.AddInput(new JoystickButtonInput(4, 19, KeyCode.Joystick5Button19));

    UInput.AddInput(new JoystickButtonInput(5, 0,  KeyCode.Joystick6Button0));
    UInput.AddInput(new JoystickButtonInput(5, 1,  KeyCode.Joystick6Button1));
    UInput.AddInput(new JoystickButtonInput(5, 2,  KeyCode.Joystick6Button2));
    UInput.AddInput(new JoystickButtonInput(5, 3,  KeyCode.Joystick6Button3));
    UInput.AddInput(new JoystickButtonInput(5, 4,  KeyCode.Joystick6Button4));
    UInput.AddInput(new JoystickButtonInput(5, 5,  KeyCode.Joystick6Button5));
    UInput.AddInput(new JoystickButtonInput(5, 6,  KeyCode.Joystick6Button6));
    UInput.AddInput(new JoystickButtonInput(5, 7,  KeyCode.Joystick6Button7));
    UInput.AddInput(new JoystickButtonInput(5, 8,  KeyCode.Joystick6Button8));
    UInput.AddInput(new JoystickButtonInput(5, 9,  KeyCode.Joystick6Button9));
    UInput.AddInput(new JoystickButtonInput(5, 10, KeyCode.Joystick6Button10));
    UInput.AddInput(new JoystickButtonInput(5, 11, KeyCode.Joystick6Button11));
    UInput.AddInput(new JoystickButtonInput(5, 12, KeyCode.Joystick6Button12));
    UInput.AddInput(new JoystickButtonInput(5, 13, KeyCode.Joystick6Button13));
    UInput.AddInput(new JoystickButtonInput(5, 14, KeyCode.Joystick6Button14));
    UInput.AddInput(new JoystickButtonInput(5, 15, KeyCode.Joystick6Button15));
    UInput.AddInput(new JoystickButtonInput(5, 16, KeyCode.Joystick6Button16));
    UInput.AddInput(new JoystickButtonInput(5, 17, KeyCode.Joystick6Button17));
    UInput.AddInput(new JoystickButtonInput(5, 18, KeyCode.Joystick6Button18));
    UInput.AddInput(new JoystickButtonInput(5, 19, KeyCode.Joystick6Button19));

    UInput.AddInput(new JoystickButtonInput(6, 0,  KeyCode.Joystick7Button0));
    UInput.AddInput(new JoystickButtonInput(6, 1,  KeyCode.Joystick7Button1));
    UInput.AddInput(new JoystickButtonInput(6, 2,  KeyCode.Joystick7Button2));
    UInput.AddInput(new JoystickButtonInput(6, 3,  KeyCode.Joystick7Button3));
    UInput.AddInput(new JoystickButtonInput(6, 4,  KeyCode.Joystick7Button4));
    UInput.AddInput(new JoystickButtonInput(6, 5,  KeyCode.Joystick7Button5));
    UInput.AddInput(new JoystickButtonInput(6, 6,  KeyCode.Joystick7Button6));
    UInput.AddInput(new JoystickButtonInput(6, 7,  KeyCode.Joystick7Button7));
    UInput.AddInput(new JoystickButtonInput(6, 8,  KeyCode.Joystick7Button8));
    UInput.AddInput(new JoystickButtonInput(6, 9,  KeyCode.Joystick7Button9));
    UInput.AddInput(new JoystickButtonInput(6, 10, KeyCode.Joystick7Button10));
    UInput.AddInput(new JoystickButtonInput(6, 11, KeyCode.Joystick7Button11));
    UInput.AddInput(new JoystickButtonInput(6, 12, KeyCode.Joystick7Button12));
    UInput.AddInput(new JoystickButtonInput(6, 13, KeyCode.Joystick7Button13));
    UInput.AddInput(new JoystickButtonInput(6, 14, KeyCode.Joystick7Button14));
    UInput.AddInput(new JoystickButtonInput(6, 15, KeyCode.Joystick7Button15));
    UInput.AddInput(new JoystickButtonInput(6, 16, KeyCode.Joystick7Button16));
    UInput.AddInput(new JoystickButtonInput(6, 17, KeyCode.Joystick7Button17));
    UInput.AddInput(new JoystickButtonInput(6, 18, KeyCode.Joystick7Button18));
    UInput.AddInput(new JoystickButtonInput(6, 19, KeyCode.Joystick7Button19));

    UInput.AddInput(new JoystickButtonInput(7, 0,  KeyCode.Joystick8Button0));
    UInput.AddInput(new JoystickButtonInput(7, 1,  KeyCode.Joystick8Button1));
    UInput.AddInput(new JoystickButtonInput(7, 2,  KeyCode.Joystick8Button2));
    UInput.AddInput(new JoystickButtonInput(7, 3,  KeyCode.Joystick8Button3));
    UInput.AddInput(new JoystickButtonInput(7, 4,  KeyCode.Joystick8Button4));
    UInput.AddInput(new JoystickButtonInput(7, 5,  KeyCode.Joystick8Button5));
    UInput.AddInput(new JoystickButtonInput(7, 6,  KeyCode.Joystick8Button6));
    UInput.AddInput(new JoystickButtonInput(7, 7,  KeyCode.Joystick8Button7));
    UInput.AddInput(new JoystickButtonInput(7, 8,  KeyCode.Joystick8Button8));
    UInput.AddInput(new JoystickButtonInput(7, 9,  KeyCode.Joystick8Button9));
    UInput.AddInput(new JoystickButtonInput(7, 10, KeyCode.Joystick8Button10));
    UInput.AddInput(new JoystickButtonInput(7, 11, KeyCode.Joystick8Button11));
    UInput.AddInput(new JoystickButtonInput(7, 12, KeyCode.Joystick8Button12));
    UInput.AddInput(new JoystickButtonInput(7, 13, KeyCode.Joystick8Button13));
    UInput.AddInput(new JoystickButtonInput(7, 14, KeyCode.Joystick8Button14));
    UInput.AddInput(new JoystickButtonInput(7, 15, KeyCode.Joystick8Button15));
    UInput.AddInput(new JoystickButtonInput(7, 16, KeyCode.Joystick8Button16));
    UInput.AddInput(new JoystickButtonInput(7, 17, KeyCode.Joystick8Button17));
    UInput.AddInput(new JoystickButtonInput(7, 18, KeyCode.Joystick8Button18));
    UInput.AddInput(new JoystickButtonInput(7, 19, KeyCode.Joystick8Button19));

    for(int joystick=0;joystick<=11;joystick++){
      for(int axis=0;axis<=19;axis++){
        UInput.AddInput(new JoystickAxisInput(joystick, axis));
      }
    }
  }

  private void InitKeyboard() {
    UInput.AddInput(new KeyboardInput("backspace", KeyCode.Backspace));
    UInput.AddInput(new KeyboardInput("delete", KeyCode.Delete));
    UInput.AddInput(new KeyboardInput("tab", KeyCode.Tab));
    UInput.AddInput(new KeyboardInput("clear", KeyCode.Clear));
    UInput.AddInput(new KeyboardInput("return", KeyCode.Return));
    UInput.AddInput(new KeyboardInput("pause", KeyCode.Pause));
    UInput.AddInput(new KeyboardInput("escape", KeyCode.Escape));
    UInput.AddInput(new KeyboardInput("space", KeyCode.Space));
    UInput.AddInput(new KeyboardInput("input", KeyCode.Insert));
    UInput.AddInput(new KeyboardInput("home", KeyCode.Home));
    UInput.AddInput(new KeyboardInput("end", KeyCode.End));
    UInput.AddInput(new KeyboardInput("pageup", KeyCode.PageUp));
    UInput.AddInput(new KeyboardInput("pagedown", KeyCode.PageDown));

    UInput.AddInput(new KeyboardInput("numlock", KeyCode.Numlock));
    UInput.AddInput(new KeyboardInput("capslock", KeyCode.CapsLock));
    UInput.AddInput(new KeyboardInput("scrolllock", KeyCode.ScrollLock));

    UInput.AddInput(new KeyboardInput("shift_left", KeyCode.LeftShift));
    UInput.AddInput(new KeyboardInput("shift_right", KeyCode.RightShift));

    UInput.AddInput(new KeyboardInput("control_left", KeyCode.LeftControl));
    UInput.AddInput(new KeyboardInput("control_right", KeyCode.RightControl));

    UInput.AddInput(new KeyboardInput("alt_left", KeyCode.LeftAlt));
    UInput.AddInput(new KeyboardInput("alt_right", KeyCode.RightAlt));

    UInput.AddInput(new KeyboardInput("command_left", KeyCode.LeftCommand));
    UInput.AddInput(new KeyboardInput("command_right", KeyCode.RightCommand));

    UInput.AddInput(new KeyboardInput("apple_left", KeyCode.LeftApple));
    UInput.AddInput(new KeyboardInput("apple_right", KeyCode.RightApple));

    UInput.AddInput(new KeyboardInput("windows_left", KeyCode.LeftWindows));
    UInput.AddInput(new KeyboardInput("windows_right", KeyCode.RightWindows));

    UInput.AddInput(new KeyboardInput("altgr", KeyCode.AltGr));

    UInput.AddInput(new KeyboardInput("help", KeyCode.Help));
    UInput.AddInput(new KeyboardInput("print", KeyCode.Print));
    UInput.AddInput(new KeyboardInput("sysreq", KeyCode.SysReq));
    UInput.AddInput(new KeyboardInput("break", KeyCode.Break));
    UInput.AddInput(new KeyboardInput("menu", KeyCode.Menu));

    UInput.AddInput(new KeyboardInput("numpad_0", KeyCode.Keypad0));
    UInput.AddInput(new KeyboardInput("numpad_1", KeyCode.Keypad1));
    UInput.AddInput(new KeyboardInput("numpad_2", KeyCode.Keypad2));
    UInput.AddInput(new KeyboardInput("numpad_3", KeyCode.Keypad3));
    UInput.AddInput(new KeyboardInput("numpad_4", KeyCode.Keypad4));
    UInput.AddInput(new KeyboardInput("numpad_5", KeyCode.Keypad5));
    UInput.AddInput(new KeyboardInput("numpad_6", KeyCode.Keypad6));
    UInput.AddInput(new KeyboardInput("numpad_7", KeyCode.Keypad7));
    UInput.AddInput(new KeyboardInput("numpad_8", KeyCode.Keypad8));
    UInput.AddInput(new KeyboardInput("numpad_9", KeyCode.Keypad9));

    UInput.AddInput(new KeyboardInput("numpad_period", KeyCode.KeypadPeriod));
    UInput.AddInput(new KeyboardInput("numpad_divide", KeyCode.KeypadDivide));
    UInput.AddInput(new KeyboardInput("numpad_multiply", KeyCode.KeypadMultiply));
    UInput.AddInput(new KeyboardInput("numpad_minus", KeyCode.KeypadMinus));
    UInput.AddInput(new KeyboardInput("numpad_plus", KeyCode.KeypadPlus));
    UInput.AddInput(new KeyboardInput("numpad_enter", KeyCode.KeypadEnter));
    UInput.AddInput(new KeyboardInput("numpad_equals", KeyCode.KeypadEquals));

    UInput.AddInput(new KeyboardInput("arrow_up", KeyCode.UpArrow));
    UInput.AddInput(new KeyboardInput("arrow_down", KeyCode.DownArrow));
    UInput.AddInput(new KeyboardInput("arrow_right", KeyCode.RightArrow));
    UInput.AddInput(new KeyboardInput("arrow_left", KeyCode.LeftArrow));

    UInput.AddInput(new KeyboardInput("f1", KeyCode.F1));
    UInput.AddInput(new KeyboardInput("f2", KeyCode.F2));
    UInput.AddInput(new KeyboardInput("f3", KeyCode.F3));
    UInput.AddInput(new KeyboardInput("f4", KeyCode.F4));
    UInput.AddInput(new KeyboardInput("f5", KeyCode.F5));
    UInput.AddInput(new KeyboardInput("f6", KeyCode.F6));
    UInput.AddInput(new KeyboardInput("f7", KeyCode.F7));
    UInput.AddInput(new KeyboardInput("f8", KeyCode.F8));
    UInput.AddInput(new KeyboardInput("f9", KeyCode.F9));
    UInput.AddInput(new KeyboardInput("f10", KeyCode.F10));
    UInput.AddInput(new KeyboardInput("f11", KeyCode.F11));
    UInput.AddInput(new KeyboardInput("f12", KeyCode.F12));
    UInput.AddInput(new KeyboardInput("f13", KeyCode.F13));
    UInput.AddInput(new KeyboardInput("f14", KeyCode.F14));
    UInput.AddInput(new KeyboardInput("f15", KeyCode.F15));

    UInput.AddInput(new KeyboardInput("0", KeyCode.Alpha0));
    UInput.AddInput(new KeyboardInput("1", KeyCode.Alpha1));
    UInput.AddInput(new KeyboardInput("2", KeyCode.Alpha2));
    UInput.AddInput(new KeyboardInput("3", KeyCode.Alpha3));
    UInput.AddInput(new KeyboardInput("4", KeyCode.Alpha4));
    UInput.AddInput(new KeyboardInput("5", KeyCode.Alpha5));
    UInput.AddInput(new KeyboardInput("6", KeyCode.Alpha6));
    UInput.AddInput(new KeyboardInput("7", KeyCode.Alpha7));
    UInput.AddInput(new KeyboardInput("8", KeyCode.Alpha8));
    UInput.AddInput(new KeyboardInput("9", KeyCode.Alpha9));

    UInput.AddInput(new KeyboardInput("exclaim", KeyCode.Exclaim));
    UInput.AddInput(new KeyboardInput("double_quote", KeyCode.DoubleQuote));
    UInput.AddInput(new KeyboardInput("hash", KeyCode.Hash));
    UInput.AddInput(new KeyboardInput("dollar", KeyCode.Dollar));
    UInput.AddInput(new KeyboardInput("ampersand", KeyCode.Ampersand));
    UInput.AddInput(new KeyboardInput("quote", KeyCode.Quote));
    UInput.AddInput(new KeyboardInput("paren_left", KeyCode.LeftParen));
    UInput.AddInput(new KeyboardInput("paren_right", KeyCode.RightParen));
    UInput.AddInput(new KeyboardInput("asterisk", KeyCode.Asterisk));
    UInput.AddInput(new KeyboardInput("plus", KeyCode.Plus));
    UInput.AddInput(new KeyboardInput("comma", KeyCode.Comma));
    UInput.AddInput(new KeyboardInput("minus", KeyCode.Minus));
    UInput.AddInput(new KeyboardInput("period", KeyCode.Period));
    UInput.AddInput(new KeyboardInput("slash", KeyCode.Slash));
    UInput.AddInput(new KeyboardInput("colon", KeyCode.Colon));
    UInput.AddInput(new KeyboardInput("semicolon", KeyCode.Semicolon));
    UInput.AddInput(new KeyboardInput("less", KeyCode.Less));
    UInput.AddInput(new KeyboardInput("equals", KeyCode.Equals));
    UInput.AddInput(new KeyboardInput("greater", KeyCode.Greater));
    UInput.AddInput(new KeyboardInput("question", KeyCode.Question));
    UInput.AddInput(new KeyboardInput("at", KeyCode.At));
    UInput.AddInput(new KeyboardInput("bracket_left", KeyCode.LeftBracket));
    UInput.AddInput(new KeyboardInput("bracket_right", KeyCode.RightBracket));

    UInput.AddInput(new KeyboardInput("backslash", KeyCode.Backslash));
    UInput.AddInput(new KeyboardInput("caret", KeyCode.Caret));
    UInput.AddInput(new KeyboardInput("underscore", KeyCode.Underscore));
    UInput.AddInput(new KeyboardInput("backquote", KeyCode.BackQuote));

    UInput.AddInput(new KeyboardInput("a", KeyCode.A));
    UInput.AddInput(new KeyboardInput("b", KeyCode.B));
    UInput.AddInput(new KeyboardInput("c", KeyCode.C));
    UInput.AddInput(new KeyboardInput("d", KeyCode.D));
    UInput.AddInput(new KeyboardInput("e", KeyCode.E));
    UInput.AddInput(new KeyboardInput("f", KeyCode.F));
    UInput.AddInput(new KeyboardInput("g", KeyCode.G));
    UInput.AddInput(new KeyboardInput("h", KeyCode.H));
    UInput.AddInput(new KeyboardInput("i", KeyCode.I));
    UInput.AddInput(new KeyboardInput("j", KeyCode.J));
    UInput.AddInput(new KeyboardInput("k", KeyCode.K));
    UInput.AddInput(new KeyboardInput("l", KeyCode.L));
    UInput.AddInput(new KeyboardInput("m", KeyCode.M));
    UInput.AddInput(new KeyboardInput("n", KeyCode.N));
    UInput.AddInput(new KeyboardInput("o", KeyCode.O));
    UInput.AddInput(new KeyboardInput("p", KeyCode.P));
    UInput.AddInput(new KeyboardInput("q", KeyCode.Q));
    UInput.AddInput(new KeyboardInput("r", KeyCode.R));
    UInput.AddInput(new KeyboardInput("s", KeyCode.S));
    UInput.AddInput(new KeyboardInput("t", KeyCode.T));
    UInput.AddInput(new KeyboardInput("u", KeyCode.U));
    UInput.AddInput(new KeyboardInput("v", KeyCode.V));
    UInput.AddInput(new KeyboardInput("w", KeyCode.W));
    UInput.AddInput(new KeyboardInput("x", KeyCode.X));
    UInput.AddInput(new KeyboardInput("y", KeyCode.Y));
    UInput.AddInput(new KeyboardInput("z", KeyCode.Z));
  }

  private void InitMouse() {
    UInput.AddInput(new MouseButtonInput(0, KeyCode.Mouse0));
    UInput.AddInput(new MouseButtonInput(1, KeyCode.Mouse1));
    UInput.AddInput(new MouseButtonInput(2, KeyCode.Mouse2));
    UInput.AddInput(new MouseButtonInput(3, KeyCode.Mouse3));
    UInput.AddInput(new MouseButtonInput(4, KeyCode.Mouse4));
    UInput.AddInput(new MouseButtonInput(5, KeyCode.Mouse5));
    UInput.AddInput(new MouseButtonInput(6, KeyCode.Mouse6));
  }

  private void UpdateInputs() {
    inputs.ForEach(delegate(InputWrapper input) {
      input.Step();
    });
  }
}

public class InputWrapper {
  protected Hashtable defaultPayload = new Hashtable();
  private float current = 0;
  private float previous = 0;

  private string _inputName = "unnamed";
  public string inputName{get{return this._inputName;}}

  private bool _isBinary = false;
  public bool isBinary{get{return this._isBinary;}}
  public InputWrapper(string inputName, bool isBinary) {
    this._isBinary = isBinary;
    this._inputName = inputName;
  }

  public void Step(){
    this.Update();
    this.Trigger();
  }

  public void Trigger() {
    float state = this.State();
    float oldState = state - this.Delta();

    Hashtable payload = new Hashtable(defaultPayload);
    payload.Set("input", this.inputName);
    payload.Set("state", state);
    payload.Set("delta", this.Delta());

    if(this.isBinary){

      float stateBinary = Mathf.Round(state);
      float oldStateBinary = Mathf.Round(oldState);

      payload.Set("state", state);

      if(oldStateBinary != stateBinary) {
        if(stateBinary == 0) {
          UInput.WithInstance().Emit(this.inputName + ".released", payload);
        } else {
          UInput.WithInstance().Emit(this.inputName + ".pressed", payload);
        }
      } else {
        if(stateBinary == 1) {
          UInput.WithInstance().Emit(this.inputName + ".held", payload);
        }
      }
    } else {
      if(this.Delta() != 0) {
        UInput.WithInstance().Emit(this.inputName + ".changed", payload);
      }
    }
  }

  public virtual void Update() {}

  public void Set(float value) {
    this.previous = this.current;
    this.current = value;
  }

  public virtual float State() {

    return this.current;
  }

  public virtual float Delta() {

    return this.previous - this.current;
  }
}

public class CatagoryInput : InputWrapper {

  private string _catagory;
  public string catagory{get{return catagory;}}

  public CatagoryInput(string catagory, string inputName, bool isBinary) : base(catagory+"."+inputName, isBinary) {
    this._catagory = catagory;
  }
}

class KeyCodeInput : CatagoryInput {

  private KeyCode _keyCode;
  public KeyCode keyCode {get {return _keyCode;}}

  public KeyCodeInput(string catagory, string inputName, KeyCode keyCode) : base(catagory, inputName, true) {
    this._keyCode = keyCode;
    this.defaultPayload.Set("keyCode", keyCode);
  }

  public override void Update() {
    this.Set( Input.GetKey(this.keyCode) ? 1.0f : 0.0f );
  }
}

class MouseButtonInput : KeyCodeInput {
  private int _button;
  public int button {get {return _button;}}

  public MouseButtonInput(int button, KeyCode keyCode) : base("mouse", "button."+button, keyCode) {
    this._button = button;
    this.defaultPayload.Set("button", button);
  }
}

class KeyboardInput : KeyCodeInput {
  public KeyboardInput(string inputName, KeyCode keyCode) : base("keyboard", inputName, keyCode) {

  }
}

class JoystickButtonInput : KeyCodeInput {
  private int _joystick;
  public int joystick{get {return joystick;}}

  private int _button;
  public int button{get {return button;}}

  public JoystickButtonInput(int joystick, int button, KeyCode keyCode) : base("joystick", joystick+ ".button." +button, keyCode){
    this._joystick = joystick;
    this.defaultPayload.Set("joystick", joystick);
  }
}

class AxisInput : CatagoryInput {
  private string _axisName;
  public string axisName{
    get{
      return _axisName;
    }
  }

  public AxisInput(string catagory, string inputName, string axisName) : base(catagory, inputName, false) {
    this._axisName = axisName;
    this.defaultPayload.Set("axisName", axisName);
  }

  public override void Update() {
    this.Set(Input.GetAxis(this.axisName));
  }

}

class JoystickAxisInput : AxisInput {
  private int _joystick;
  public int joystick{get{return _joystick;}}

  private int _axis;
  public int axis{get{return _axis;}}

  public JoystickAxisInput(int joystick, int axis) : base("joystick", joystick+ ".axis." +axis, axis+ "-" +joystick) {
    this._joystick = joystick;
    this._axis = axis;
    this.defaultPayload.Set("joystick", joystick);
    this.defaultPayload.Set("axis", axis);
  }

}

// class MouseInput : CatagoryInput {
//   private int _axis;
//   public int axis{get{return _axis;}}

//   public MouseInput(string catagory, string inputName, int axis) : base(catagory, inputName, false) {
//     this._axis = axis==0 ? 0 : 1;
//     this.defaultPayload.Set("axis", this._axis);
//   }

//   public override void Update() {
//     this.Set(Input.mousePosition[axis == 0 ? 0 : 2]);
//   }
// }

public class BindInput : CatagoryInput
{
  private Dictionary<string, float> bindings = new Dictionary<string, float>();

  public BindInput(string catagory, string inputName, bool isBinary) : base(catagory, inputName, isBinary) { }

  public void Bind(string eventName)
  {
    this.Bind(eventName, 1f);
  }

  public void Bind(string eventName, float factor)
  {
    this.bindings[eventName] = factor;
  }

  public void Unbind(string binding)
  {
    this.bindings.Remove(binding);
  }

  public override void Update()
  {
    this.Set(this.State());
  }

  public override float State()
  {
    float best = 0;

    foreach(KeyValuePair<string, float> binding in bindings)
    {
      InputWrapper input = UInput.Get(binding.Key);
      float state = input.State();

      if(Mathf.Abs(state) >= Mathf.Abs(best))
      {
        best = state * binding.Value;
      }
    }

    return best;
  }
}
