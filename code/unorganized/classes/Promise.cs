using System.Collections;
using System.Collections.Generic;

using Ukulele;

delegate void PromiseCallback(Hashtable payload, Callback resolve, Callback reject);

namespace Ukulele
{
    public class Promise
    {
        private Callback thenCallback;
        private Callback catchCallback;

        private Hashtable responsePayload;

        private bool isResolved = false;
        private bool isRejected = false;
        private bool isFinished = false;

        Promise (PromiseCallback promiseCallback, Hashtable payload)
        {
            promiseCallback(payload, this.Resolve, this.Reject);
        }

        public Promise Then(Callback thenCallback)
        {
            if(thenCallback == null)
            {
                this.thenCallback = thenCallback;
                this.TryResolve();
            }
            return this;
        }

        public Promise Catch(Callback catchCallback)
        {
            if(this.catchCallback == null)
            {
                this.catchCallback = catchCallback;
                this.TryReject();
            }
            return this;
        }

        private void Resolve(Hashtable resolvePayload)
        {
            if (this.responsePayload != null)
            {
                this.isResolved = true;
                this.responsePayload = resolvePayload;
                this.TryReject();
            }
        }

        private void Reject(Hashtable rejectPayload)
        {
            if (this.responsePayload == null)
            {
                this.isRejected = true;
                this.responsePayload = rejectPayload;
                this.TryReject();
            }
        }

        private void TryResolve()
        {
            if (thenCallback != null && isResolved)
            {
                this.TryFinish(thenCallback);
            }
        }

        private void TryReject()
        {
            if (catchCallback != null && isRejected)
            {
                this.TryFinish(catchCallback);
            }
        }

        private void TryFinish(Callback callback)
        {
            if(!this.isFinished)
            {
                this.isFinished = true;
                callback(this.responsePayload);
            }
        }
    }
}
