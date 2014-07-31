package ar.com.gmn.android.fragment;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Vector;

import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import ar.com.gmn.android.R;

public class StatusFragment extends Fragment implements OnClickListener {
	private Map<String, List<TextView>> rows;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		rows = new HashMap<String, List<TextView>>();
		View view = inflater.inflate(R.layout.fragment_help_table, null);
		Typeface type = Typeface.createFromAsset(view.getContext().getAssets(),
				"fonts/HandWrite.ttf");
		TableLayout tl = (TableLayout) view.findViewById(R.id.help_table);
		TableRow tr;

		((TextView) view.findViewById(R.id.pos0Header)).setTypeface(type);
		((TextView) view.findViewById(R.id.pos1Header)).setTypeface(type);
		((TextView) view.findViewById(R.id.pos2Header)).setTypeface(type);
		((TextView) view.findViewById(R.id.pos3Header)).setTypeface(type);
		((TextView) view.findViewById(R.id.pos4Header)).setTypeface(type);
		TextView tx;
		for (int i = 0; i < 10; i++) {
			rows.put(String.valueOf(i), new Vector<TextView>());
			tr = new TableRow(view.getContext());

			tr.addView(createEmptyText(view, type, String.valueOf(i)));
			tx = createEmptyText(view, type, "");
			rows.get(String.valueOf(i)).add(tx);
			tr.addView(tx);
			
			tx = createEmptyText(view, type, "");
			rows.get(String.valueOf(i)).add(tx);
			tr.addView(tx);
			
			tx = createEmptyText(view, type, "");
			rows.get(String.valueOf(i)).add(tx);
			tr.addView(tx);
			
			tx = createEmptyText(view, type, "");
			rows.get(String.valueOf(i)).add(tx);
			tr.addView(tx);

			tl.addView(tr);
			
		}

		return view;
	}

	public TextView createEmptyText(View view, Typeface type, String text) {
		TextView tx;
		tx = new TextView(view.getContext());
		tx.setGravity(Gravity.CENTER);
		tx.setTextAppearance(view.getContext(), R.style.CellHelpTable);
		tx.setBackgroundResource(R.drawable.cell_borders);
		tx.setTypeface(type);
		tx.setOnClickListener(this);
		tx.setText(text);
		return tx;
	}

	@Override
	public void onClick(View v) {
		TextView t = (TextView)v;
		if(t.getText().equals("")){
			t.setText("X");
			t.setTextColor(Color.RED);
		}else if(t.getText().equals("X")){
			t.setText("?");
			t.setTextColor(Color.parseColor("#ffDBA901"));
		}else if(t.getText().equals("?")){
			t.setText("");
		}else{
			
			for (TextView tx : rows.get(t.getText())) {
				tx.setText("X");
				tx.setTextColor(Color.RED);
			}
		}
	}
}
