delegate void PromiseCallback(UData payload, Callback resolve, Callback reject);

public class UPromise {
  private Callback thenCallback;
  private Callback catchCallback;

  private UData responsePayload;

  private bool isResolved = false;
  private bool isRejected = false;
  private bool isFinished = false;

  UPromise (PromiseCallback promiseCallback, UData payload)
  {
    promiseCallback(payload, this.Resolve, this.Reject);
  }

  public UPromise Then(Callback thenCallback) {
    if(thenCallback == null) {
      this.thenCallback = thenCallback;
      this.TryResolve();
    }
    return this;
  }

  public UPromise Catch(Callback catchCallback) {
    if(this.catchCallback == null) {
      this.catchCallback = catchCallback;
      this.TryReject();
    }
    return this;
  }

  private void Resolve(UData resolvePayload) {
    if (this.responsePayload != null) {
      this.isResolved = true;
      this.responsePayload = resolvePayload;
      this.TryReject();
    }
  }

  private void Reject(UData rejectPayload) {
    if (this.responsePayload == null) {
      this.isRejected = true;
      this.responsePayload = rejectPayload;
      this.TryReject();
    }
  }

  private void TryResolve() {
    if (thenCallback != null && isResolved) {
      this.TryFinish(thenCallback);
    }
  }

  private void TryReject() {
    if (catchCallback != null && isRejected) {
      this.TryFinish(catchCallback);
    }
  }

  private void TryFinish(Callback callback) {
    if(!this.isFinished) {
      this.isFinished = true;
      callback(this.responsePayload);
    }
  }
}
