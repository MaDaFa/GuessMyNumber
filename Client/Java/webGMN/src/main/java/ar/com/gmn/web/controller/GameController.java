package ar.com.gmn.web.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
@Controller
public class GameController {
	@RequestMapping(value="/hello")  
    public String goToHelloPage() {  
       
          
        return "hello";  
    }  
}
