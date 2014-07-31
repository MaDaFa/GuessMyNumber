package ar.com.gmn.android.view.component;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Color;
import android.graphics.Typeface;
import android.util.AttributeSet;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

import ar.com.gmn.android.R;
import ar.com.gmn.android.fragment.DuelFragment;
import ar.com.gmn.android.fragment.SinglePlayerFragment;

public class CustomNumberPicker2 extends LinearLayout {
private TextView upArrow;
private TextView downArrow;
private TextView numero;
private Integer current;
private Typeface typeFace;

//private ImageViewOnTouchListener touchListerner;

    public CustomNumberPicker2(Context context, AttributeSet attrs){
        super(context,attrs);
        initView(context);
        TypedArray a = context.obtainStyledAttributes(attrs,
                R.styleable.CustomNumberPicker2);

        if(!this.isInEditMode()) {
            for (int i = 0; i < a.getIndexCount(); ++i) {

                int attr = a.getIndex(i);
                switch (attr) {

                  case R.styleable.CustomNumberPicker2_styleId:
                        switch (a.getInteger(attr,-1)){
                            case DuelFragment.NUMBERPIKER_STYLE:
                                setNumberStyle(R.style.DueloNumberPiker_Number);
                                setButtonStyle(R.style.DueloNumberPiker_Button);
                                setNumberFrame(R.drawable.border);
                                typeFace = Typeface.createFromAsset(context.getAssets(),
                                        "fonts/HandWrite.ttf");
                                this.setButtonTypeFace(typeFace);
                                this.setNumberTypeFace(typeFace);
                                break;
                            case SinglePlayerFragment.NUMBERPIKER_STYLE:
                                setNumberStyle(R.style.SP_NumberPiker_Number);
                                setButtonStyle(R.style.SP_NumberPiker_Button);
                                setNumberFrame(R.drawable.chalk_frame);
                                typeFace = Typeface.createFromAsset(context.getAssets(),
                                        "fonts/EraserDust.ttf");
                                this.setButtonTypeFace(typeFace);
                                this.setNumberTypeFace(typeFace);
                                break;
                        }
                        break;


                }
            }

            a.recycle();
        }
        initView(context);
    }


	public CustomNumberPicker2(Context context) {
		super(context);
        initView(context);
		/*createUpButton(context);
		createDownButton(context);

		createNumero(context);*/
	}

    private void initView(Context context) {
        final LayoutInflater inflater = (LayoutInflater)
                context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if(inflater != null){
            inflater.inflate(R.layout.custom_number_piker, this);
        }
        current = 0;
        upArrow = (TextView)findViewById(R.id.upArrow);

        downArrow  = (TextView)findViewById(R.id.downArrow);
        numero = (TextView)findViewById(R.id.number);
        upArrow.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                numero.setText(getNext().toString());

            }


        });
        downArrow.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                numero.setText(getPrevious().toString());

            }

        });
    }

    public void createNumero(Context context) {


		numero.setText(current.toString());
		numero.setGravity(Gravity.CENTER);

	}

	public void createUpButton(Context context) {



		upArrow.setText("+");
		upArrow.setGravity(Gravity.CENTER);


	}

	public void createDownButton(Context context) {

		downArrow.setText("-");
		downArrow.setGravity(Gravity.CENTER);

//		downArrow.setOnTouchListener(this.touchListerner);
	}
	
	private Integer getNext() {
		if (current<9){
			current++;
		
		}else{
			current = 0;
		}
		return current;
		
	}
	private Integer getPrevious() {
		if (current>0){
			current--;
		
		}else{
			current = 9;
		}
		return current;
		
	}
	public Integer getValue() {
		return current;
	}
	public void setCorrecto(){
		
		this.numero.setShadowLayer(10, 0, 0, Color.GREEN);
		
	}
	public void setIncorrecto(Integer numero){
		
		this.numero.setText(numero.toString());
		this.numero.setShadowLayer(10, 0, 0, Color.RED);
	}

	public void setButtonStyle(int style) {
		this.upArrow.setTextAppearance(this.getContext(), style);
		this.downArrow.setTextAppearance(this.getContext(), style);
		
	}

	public void setNumberStyle(int style) {
		this.numero.setTextAppearance(this.getContext(), style);
		
	}

	public void setNumberFrame(int border) {
		this.numero.setBackgroundResource(border);
		
	}
	public void setButtonFrame(int border) {
		this.upArrow.setBackgroundResource(border);
		this.downArrow.setBackgroundResource(border);
		
	}

	public void setButtonTypeFace(Typeface tf) {
		this.upArrow.setTypeface(tf);
		this.downArrow.setTypeface(tf);
		
	}

	public void setNumberTypeFace(Typeface tf) {
		this.numero.setTypeface(tf);
		
	}
}
