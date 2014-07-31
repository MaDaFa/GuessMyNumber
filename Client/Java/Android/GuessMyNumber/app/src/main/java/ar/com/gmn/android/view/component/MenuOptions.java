package ar.com.gmn.android.view.component;

import java.util.List;
import java.util.Vector;

import android.content.Context;
import ar.com.gmn.android.DuelFragmentActivity;
import ar.com.gmn.android.MultiplayerManagerActivity;
import ar.com.gmn.android.SinglePlayerFragmentActivity;
import ar.com.gmn.android.core.IMenuOptions;

public class MenuOptions implements IMenuOptions {
private List<MenuOption> options;
	@Override
	public List<MenuOption> getOptions() {
		// TODO Auto-generated method stub
		return options;
	}
	
	public MenuOptions(Context context, int style){
		this.options = new Vector<MenuOption>();
		MenuOption m = new MenuOption(context, "Single PLayer", SinglePlayerFragmentActivity.class, style);
		this.options.add(m);
		m = new MenuOption(context, "Duelo", MultiplayerManagerActivity.class,style);
		this.options.add(m);
//		m = new MenuOption(context, "Config", DuelFragmentActivity.class,style);
//		this.options.add(m);
		
		
	}

}
