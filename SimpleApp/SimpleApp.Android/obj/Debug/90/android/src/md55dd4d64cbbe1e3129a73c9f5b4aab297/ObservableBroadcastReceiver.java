package md55dd4d64cbbe1e3129a73c9f5b4aab297;


public class ObservableBroadcastReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Acr.ObservableBroadcastReceiver, Acr.Core", ObservableBroadcastReceiver.class, __md_methods);
	}


	public ObservableBroadcastReceiver ()
	{
		super ();
		if (getClass () == ObservableBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("Acr.ObservableBroadcastReceiver, Acr.Core", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
