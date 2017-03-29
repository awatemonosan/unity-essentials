public class UPromise {

  delegate void PromiseCallback(UModel model, Callback resolve, Callback reject);

  UPromise (PromiseCallback promiseCallback)
  {
    promiseCallback()
    this.promiseCallback = promiseCallback;
  }

  public Thenable Then()

  void Thenable Promise(UModel model)
  {
    Thenable thenable = new Thenable();
    this.promiseCallback(model, thenable);
    return thenable;
  }
}

public class Thenable()
  private isDone = false;
  private isResolved = false;
  private isRejected = false;

  public void Resolve(UModel resolution)
  {
    if (this.isDone) return;
    this.isDone = true;
    this.isResolved = true;
  }

  public void Reject(UModel reason)
  {
    if (this.isDone) return;
    this.isDone = true;
    this.isRejected = true;
  }

  public void Then(Callback callback)
  {

  }
