package ComputerGraphicsDDA;
import javax.swing.*;
import java.awt.*;
import java.awt.geom.Line2D;
import java.util.ArrayList;

public class CGPanel extends JPanel {

    private final int pix = 10;      // 픽셀 1개 가로세로 크기
    private final int numX = 30;   // 제1사분면 x 좌표 범위
    private final int numY = 30;   // 제1사분면 y 좌표 범위
    private final int sizeX = numX*pix; // 모눈종이 x축 크기
    private final int sizeY = numY*pix; // 모눈종이 y축 크기
    
    private ArrayList<xypos> arr;
    private ArrayList<xypos> arr2; //bresenham.
    private ArrayList<xypos> arr3; //circle.
    private panel_graph gpanel;
    
    private int a1,a2,b1,b2; //입력 받은 점들
    private int a11, a22, b11, b22; //bresenham.
    private int ca1,ca2,cr; //circle.

    CGPanel() {
        setLayout(new FlowLayout());
        gpanel = new panel_graph();
        add(gpanel);
        add(new panel_control());
        arr = new ArrayList<>();
        arr2 = new ArrayList<>();
        arr3 = new ArrayList<>();
    }

    //(x,y)좌표 점 클래스
    class xypos{
        int x, y;
        public xypos(int x, int y){
            this.x = x;
            this.y = y;
        }
    }

    class panel_graph extends JPanel{
        panel_graph(){
            //모눈종이 끝 선 안잘리게 패널 크기를 모눈종이보다 조금 더 크게 한다.
            setPreferredSize(new Dimension((sizeX+1)*2, (sizeY+1)*2));
        }

        public void paintComponent(Graphics g){     // g는 GraphicsContext 객체
            super.paintComponent(g);
            Graphics2D g2 = (Graphics2D)g;
            g2.translate(sizeX,sizeY);      // 원점을 (sizeX,sizeY)로 이동.
            g2.setColor(Color.BLACK);
            //모눈종이 가로세로줄 그리기
            //좌표평면 그리기
            g2.setStroke(new BasicStroke(1));  //stroke : 도형 테두리 설정. 내부채우기설정은 setFill.
            for(int i=-sizeY; i<=sizeY; i+=pix){
                g2.drawLine(-sizeX, i, sizeX, i);
            }
            for(int i=-sizeX; i<=sizeX; i+=pix){
                g2.drawLine(i, -sizeY, i, sizeY);
            }
            // x,y축 그리기(굵은 선)
            g2.setStroke(new BasicStroke(2));
            g2.drawLine(-sizeX, 0, sizeX, 0);
            g2.drawLine(0, -sizeY, 0, sizeY);
            //그림 그리기 - DDA
            for(xypos p : arr){
                g2.fillRect(p.x*pix, -(p.y+1)*pix, pix, pix);   // 네모는 왼쪽위에서 아래로 그림
            }
            //그림 그리기 - Bresenham.
            for(xypos p : arr2){
                g2.fillRect(p.x*pix, -(p.y+1)*pix, pix, pix);   // 네모는 왼쪽위에서 아래로 그림
            }
            //그림 그리기 - Circle.
            for(xypos p : arr3){
                g2.fillRect(p.x*pix, -(p.y+1)*pix, pix, pix);   // 네모는 왼쪽위에서 아래로 그림
            }
        }
    }
    class panel_control extends JPanel{
        //DDA
    	JTextField x1, y1, x2, y2;
        JButton btn1, btnClear;
        //Bresenham
        JTextField x11, y11, x22, y22;
        JButton btn2, btnClear2;
        //Circle
        JTextField cx,cy,r;
        JButton btn3, btnClear3;
        
        panel_control(){
            setPreferredSize(new Dimension(300, 100));
            x1 = new JTextField();  //좌표값 입력 텍스트필드
            y1 = new JTextField();
            x2 = new JTextField();
            y2 = new JTextField();
            btn1 = new JButton("DDA");
            btnClear = new JButton("Clear");
            
            x11 = new JTextField();  //좌표값 입력 텍스트필드
            y11 = new JTextField();
            x22 = new JTextField();
            y22 = new JTextField();
            btn2 = new JButton("BRSH");
            btnClear2 = new JButton("Clear");
            
            cx = new JTextField();
            cy = new JTextField();
            r = new JTextField();
            btn3 = new JButton("Circle");
            btnClear3 = new JButton("Clear");

            setLayout(new GridLayout(0, 4, 2, 2));    // 격자 형태 레이아웃
            add(new JLabel("    X1   Y1"));
            add(x1);
            add(y1);
            add(btn1);
            add(new JLabel("    X2   Y2"));
            add(x2);
            add(y2);
            add(btnClear);
            
            add(new JLabel("    X1   Y1"));
            add(x11);
            add(y11);
            add(btn2);
            add(new JLabel("    X2   Y2"));
            add(x22);
            add(y22);
            add(btnClear2);
            
            add(new JLabel("    X   Y"));
            add(cx);
            add(cy);
            add(btn3);
            add(new JLabel("    R"));
            add(r);
            add(btnClear3);

            btn1.addActionListener(e -> {
               
               a1 = Integer.parseInt(this.x1.getText());
               a2 = Integer.parseInt(this.y1.getText());
               b1 = Integer.parseInt(this.x2.getText());
               b2 = Integer.parseInt(this.y2.getText());
               
               
               xypos dot = new xypos(a1,a2);
               arr.add(dot);
               dot = new xypos (b1,b2);
               arr.add(dot);
               

               // 여기에다 DDA 알고리즘 호출
               DDA();
                gpanel.repaint();
            });
            btnClear.addActionListener(e -> {
                arr.clear();    //좌표리스트 초기화
                gpanel.repaint();
            });
            
            btn2.addActionListener(e -> {
                
                a11 = Integer.parseInt(this.x11.getText());
                a22 = Integer.parseInt(this.y11.getText());
                b11 = Integer.parseInt(this.x22.getText());
                b22 = Integer.parseInt(this.y22.getText());
                
                
                xypos dot = new xypos(a11,a22);
                arr2.add(dot);
                dot = new xypos (b11,b22);
                arr2.add(dot);
             // 여기에다 BRSH 알고리즘 호출
                BRSH();
                gpanel.repaint();
             });
             btnClear2.addActionListener(e -> {
                 arr2.clear();    //좌표리스트 초기화
                 gpanel.repaint();
             });
             
             btn3.addActionListener(e -> {
                 
                 ca1 = Integer.parseInt(this.cx.getText());
                 ca2 = Integer.parseInt(this.cy.getText());
                 cr = Integer.parseInt(this.r.getText());
                 
                 
                 xypos dot = new xypos(ca1,ca2);
                 arr3.add(dot);
                 // 여기에다 CIRC 알고리즘 호출
                 //CIRC();
                 gpanel.repaint();
              });
              btnClear3.addActionListener(e -> {
                  arr3.clear();    //좌표리스트 초기화
                  gpanel.repaint();
              });
        }
        
        public void BRSH()
        {
        	xypos dot1 = arr2.get(0);
        	xypos dot2 = arr2.get(1);
        	
        	//delta x, delta y값 계산
        	int dx = dot1.x - dot2.x;
        	int dy = dot1.y - dot2.y;
        	
        	//기울기 m값 계산
        	float m = (float) dy/dx;
        	
        	//기울기 계산 후 dx, dy 값 모두 양수로 (절댓값)
        	if (dx<0)
        		dx *= -1;
        	if (dy<0)
        		dy *= -1;
        	
        	//기울기 |m|<1
        	//0 ≤ m ≤ 1 일 때
        	if ( 0<= m && m <= 1)
        	{
        		//x좌표가 더 작은 점을 dot1으로 swap
            	if (dot1.x > dot2.x)
            	{
            		xypos tmp = dot1;
            		dot1 = dot2;
            		dot2 = tmp;
            	}
            	
        		int p = 2*dy - dx; //판정식 p0.
        		int tmpy = dot1.y;
        		
        		for (int i = dot1.x+1; i< dot2.x; i++)
        		{
        			if (p>=0) //판정식 p가 0보다 클 때.
        			{
        				tmpy++;
        				xypos ndot = new xypos(i,tmpy);
        				arr2.add(ndot);
        				p = p + 2*(dy-dx);
        			}
        			else
        			{
        				xypos ndot = new xypos(i,tmpy);
        				arr2.add(ndot);
        				p = p + 2*dy;
        			}
        		}
        	}
        	
        	//-1 ≤ m < 0 일 때
        	if ( -1<= m && m < 0)
        	{
        		//x좌표가 더 작은 점을 dot1으로 swap
            	if (dot1.x > dot2.x)
            	{
            		xypos tmp = dot1;
            		dot1 = dot2;
            		dot2 = tmp;
            	}
            	
        		int p = 2*dy - dx; //판정식 p0.
        		int tmpy = dot2.y;
        		
        		for (int i = dot2.x-1; i> dot1.x; i--)
        		{
        			if (p>=0) //판정식 p가 0보다 클 때.
        			{
        				tmpy++;
        				xypos ndot = new xypos(i,tmpy);
        				arr2.add(ndot);
        				p = p + 2*(dy-dx);
        			}
        			else
        			{
        				xypos ndot = new xypos(i,tmpy);
        				arr2.add(ndot);
        				p = p + 2*dy;
        			}
        		}
        	}
        	
        	//기울기 |m|>1 ->판정식에서 dx, dy를 바꿔준다.
        	System.out.println("m = "+m);
        	// 1<m일 때.
        	if (1<m)
        	{
        		//y좌표가 더 작은 점을 dot1으로 swap
            	if (dot1.y > dot2.y)
            	{
            		xypos tmp = dot1;
            		dot1 = dot2;
            		dot2 = tmp;
            	}
            	
        		int p = 2*dx - dy; //판정식 p0.
        		int tmpx = dot1.x;
        		
        		for (int i = dot1.y+1; i< dot2.y; i++)
        		{
        			if (p>=0) //판정식 p가 0보다 클 때.
        			{
        				tmpx++;
        				xypos ndot = new xypos(tmpx,i);
        				arr2.add(ndot);
        				p = p + 2*(dx-dy);
        				System.out.println("p= "+p);
        			}
        			else
        			{
        				xypos ndot = new xypos(tmpx,i);
        				arr2.add(ndot);
        				p = p + 2*dx;
        			}
        		}
            }
        	
        	if (m<-1)
        	{
        		//y좌표가 더 작은 점을 dot1으로 swap
            	if (dot1.y > dot2.y)
            	{
            		xypos tmp = dot1;
            		dot1 = dot2;
            		dot2 = tmp;
            	}
            	
        		int p = 2*dx - dy; //판정식 p0.
        		int tmpx = dot2.x;
        		
        		for (int i = dot2.y-1; i> dot1.y; i--)
        		{
        			if (p>=0) //판정식 p가 0보다 클 때.
        			{
        				tmpx++;
        				xypos ndot = new xypos(tmpx,i);
        				arr2.add(ndot);
        				p = p + 2*(dx-dy);
        			}
        			else
        			{
        				xypos ndot = new xypos(tmpx,i);
        				arr2.add(ndot);
        				p = p + 2*dx;
        			}
        		}
        	}
        }
        	
        
        
        public void DDA()
        {
           xypos dot1 = arr.get(0);
           xypos dot2 = arr.get(1);
           
           //delta x, delta y값 계산
           int dx = dot1.x-dot2.x;
           int dy = dot1.y-dot2.y;
           
           //기울기 값 계산
           float m = (float) dy/dx; 
           
           //기울기 m>1 일 때, y는 1씩 증가, x는 1/m씩 증가. (y-dominant)
           if (m>1)
           {
              //x1,y1 값이  x2,y2 값보다 작을 때 (ex: x1,y1 = (0,0), x2,y2 = (8,10))
              if (dot1.x <dot2.x && dot1.y < dot2.y)
              {
                 float xi = (float)dot1.x;
              
                 for (int i = dot1.y; i <= dot2.y; i++)
                 {
                    xi += 1/m;
                    if (xi%1>4) //반올림 계산
                       xi++;
                 
                    xypos ndot = new xypos((int)xi,i);
                    arr.add(ndot);
                 }
              }
              
              //x1,y1값이 x2,y2 값보다 클 때 (ex: x1,y1 = (8,10), x2,y2 = (0,0))
              else
              {
                 float xi = (float)dot2.x;
                  
                 for (int i = dot2.y; i <= dot1.y; i++)
                 {
                    xi += 1/m;
                    if (xi%1>4) //반올림 계산
                       xi++;
                 
                    xypos ndot = new xypos((int)xi,i);
                    arr.add(ndot);
                 }
              }
           }
           
           //기울기0<=m<=1 일 때, x는 1씩 증가, y는 m씩 증가. (x-dominant)
           if (0<=m && m<=1)
           {
              //x1,y1 값이 x2,y2 값보다 작을 때 (ex: x1,y1 = (0,0), x2,y2 = (8,6))
              if (dot1.x<dot2.x && dot1.y<dot2.y)
              {
                 float yi = (float)dot1.y;
              
                 for (int i = dot1.x; i<=dot2.x; i++)
                 {
                    yi += m;
                    if (yi%1>4) //반올림 계산
                       yi++;
                 
                    xypos ndot = new xypos(i,(int)yi);
                    arr.add(ndot);
                 }
              }
              
              else
              {
                 float yi = (float)dot2.y;
              
                 for (int i = dot2.x; i<=dot1.x; i++)
                 {
                    yi += m;
                    if (yi%1>4) //반올림 계산
                       yi++;
                 
                    xypos ndot = new xypos(i,(int)yi);
                    arr.add(ndot);
                 }
              }
           }
           
           if (m>=-1) //기울기 m >=-1 일 때, 방향 반전해서 x는 1씩 감소, y는 m씩 감소. (x=dominant)
           {
              //x1<x2 이고 y1>y2인 경우(ex: x1=-4,y1=5, x2=0,y2=0)
              if (dot1.x<dot2.x && dot1.y>dot2.y)
              {
                 float yi = (float)dot2.y;
              
                 for (int i = dot2.x; i>=dot1.x; i--)
                 {
                    yi -= m;
                    if (yi%1>4) //반올림 계산
                       yi--;
             
                    xypos ndot = new xypos(i,(int)yi);
                    arr.add(ndot);
                 }
             }
              
              //x1>x2 이고 y1<y2인 경우(ex: x1=0,y1=0, x2=-4,y2=5)
              else
              {
                 float yi = (float)dot1.y;
                  
                 for (int i = dot1.x; i>=dot2.x; i--)
                 {
                    yi -= m;
                    if (yi%1>4) //반올림 계산
                       yi--;
             
                    xypos ndot = new xypos(i,(int)yi);
                    arr.add(ndot);
                 }
              }
           }
           
           //기울기 m<-1 일 때, 방향 반전해서 y는 1씩 증가, x는 1/m씩 증가. (y-dominant)
           if (m<-1)
           {
              // x1이 x2보다 작고 y1이 y2보다 큰 경우 (ex: x1 = -4, y1= 5, x2 = 0, y2 = 0)
              if (dot1.x < dot2.x && dot1.y>dot2.y)
              {   
                 float xi = (float)dot2.x;
              
                for (int i = dot2.y; i <= dot1.y; i++)
                {
                   xi += 1/m;
                   if (xi%1>4) //반올림 계산
                      xi--;
             
                   xypos ndot = new xypos((int)xi,i);
                   arr.add(ndot);
                }
              }
              
              // x1이 x2보다 크고 y1이 y2보다 작은 경우 (ex: x1 = 0, y1= 0, x2 = -4, y2 = 5)
              else
              {   
                 float xi = (float)dot1.x;
              
                for (int i = dot1.y; i <= dot2.y; i++)
                {
                   xi += 1/m;
                   if (xi%1>4) //반올림 계산
                      xi--;
             
                   xypos ndot = new xypos((int)xi,i);
                   arr.add(ndot);
                }
              }   
           }
           
        }
    }
}