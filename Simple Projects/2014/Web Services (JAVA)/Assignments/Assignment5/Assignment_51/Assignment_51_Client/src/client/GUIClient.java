package client;

import handlers.Dictionary;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.URL;

import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.border.Border;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.LineBorder;
import javax.swing.table.TableColumnModel;
import javax.swing.table.TableModel;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.util.ClientFactory;

public class GUIClient extends JFrame implements ActionListener {

	Client client = null;
	//XMLRPCClient xmlrpcClient = null;
	//IWebService myService;
	
	int testVariable = 0;
	
	Container contentPane;
	JTable table = null;
	JScrollPane spane;
	JLabel label;
	//JTextArea textBox;
	JButton addButton;
	JButton dictionaryEN;
	JButton dictionaryFI;
	
	JTextField txtf;
	JPanel textButtonPanel;
	TableModel tableModel = null;
	TableColumnModel tableColumnModel = null;
	
	JPanel loginPanel;
	JLabel l_login;
	JLabel l_password;
	JTextField txt_login;
	JTextField txt_password;
	
	JLabel emptyLabel;
	JLabel infoLabel;

	Object removableComponent = null;
	JScrollPane scrollPane = null;

	boolean tableAdded = false;

	String login;
	String password;
	
	// Translate Panel
	JLabel labelStatus;
	JComboBox comboBoxStatus;
	JLabel labelWord;
	JLabel emptyLabel2;
	JLabel emptyLabel3;
	JTextField txt_Word;
	JButton translateButton;
	JTextField txt_Translated;
	JPanel translatePanel;
	JPanel translateOptionPanel;
	JPanel translateButtonsPanel;
	
	JButton fiDictionaryButton;
	JButton enDictionaryButton;

	public GUIClient() {
		super("Assignment 5-1");
		
		contentPane = this.getContentPane();

		createLoginPanel();
		createTranslatePanel();
		
		
		contentPane.add(loginPanel, BorderLayout.CENTER);  // add login panel
		contentPane.add(textButtonPanel, BorderLayout.SOUTH);
		

		setBackground(Color.cyan);

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(400, 200);
		// Here we make the frame appear in the center of the window.
		setLocationRelativeTo(null);
		setVisible(true);
	}

	public static void main(String[] args) {

		new GUIClient();
	}


	//@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent arg0) {

		if (tableAdded) {

			System.out.println("User: " + login);
			System.out.println("Log out successfully!");
			System.out.println(" --------------------- ");
			
			contentPane.remove(translatePanel);
			
			contentPane.add(loginPanel);
			
			addButton.setText("LOG IN");
			tableAdded = false;

			repaint();
		}
		else {
			
			login = txt_login.getText();
			password = txt_password.getText();
			client = new Client(login, password);
			
			
			if(client.getLogInStatus())
			{
				System.out.println("Logged successfully as:");
				System.out.println(" -> login: " + txt_login.getText());
				System.out.println(" -> password: " + txt_password.getText());
				System.out.println(" --------------------- ");
				
				
			
				contentPane.remove(loginPanel); // login panel
			
				//scrollPane = new JScrollPane(table);
				contentPane.add(translatePanel, BorderLayout.CENTER);

				addButton.setText("LOG OUT");

				tableAdded = true;
			
				repaint();
			}
			else
			{
				infoLabel.setText("Wrong login or password!");
			}
		}
	}
	
	private void createLoginPanel() {

		// LOGIN PANEL
		loginPanel = new JPanel();
		loginPanel.setLayout(new GridLayout(3, 2));
		l_login = new JLabel("Login : ", JLabel.CENTER);
		txt_login = new JTextField();
		l_password = new JLabel("Password : ", JLabel.CENTER);
		txt_password = new JTextField();
		
		emptyLabel = new JLabel();
		infoLabel = new JLabel(" ", JLabel.CENTER);
		infoLabel.setForeground(Color.red);
		
		loginPanel.add(l_login);
		loginPanel.add(txt_login);
		loginPanel.add(l_password);
		loginPanel.add(txt_password);
		loginPanel.add(emptyLabel);
		loginPanel.add(infoLabel);
		
		
		scrollPane = new JScrollPane();
		textButtonPanel = new JPanel();
		textButtonPanel.setLayout(new GridLayout(1, 2));

		addButton = new JButton("LOG IN");
		addButton.addActionListener(this);

		//textButtonPanel.add(label);
		textButtonPanel.add(addButton);
	}
	
	private void createTranslatePanel() {

		// TRANSLATE PANEL
		translatePanel = new JPanel();
		translatePanel.setLayout(new GridLayout(5, 2));
		
		labelStatus = new JLabel("Option :    ", JLabel.RIGHT);

		comboBoxStatus = new JComboBox();
		//String a[]={"", "heinola.jpg", "barca.jpg"};
		String a []= {"EN --> FI", "FI --> EN"};
		comboBoxStatus.setModel(new DefaultComboBoxModel(a));
		
		labelWord = new JLabel("Word translated :    ", JLabel.RIGHT);
		emptyLabel2 = new JLabel("", JLabel.CENTER);
		emptyLabel3 = new JLabel("", JLabel.CENTER);
		txt_Word = new JTextField();
		
		txt_Translated = new JTextField();
		
		translateButton = new JButton("TRANSLATE");
		translateButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				if (comboBoxStatus.getSelectedIndex() == 0)
				{
					String out = client.translateToFi(txt_Word.getText());
					
					if (out == "") txt_Translated.setText("(not found)");
					else txt_Translated.setText(out);
				}
				else
				{
					String out = client.translateToEng(txt_Word.getText());
					
					if (out == "") txt_Translated.setText("(not found)");
					else txt_Translated.setText(out);
				}
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}});
		
		JButton fiDictionaryButton;
		fiDictionaryButton = new JButton("Show EN-FI DICTIONARY");
		fiDictionaryButton.setForeground(Color.BLACK);
		fiDictionaryButton.setBackground(Color.green);
		Border line = new LineBorder(Color.BLACK);
		Border margin = new EmptyBorder(5, 15, 5, 15);
		Border compound = new CompoundBorder(line, margin);
		fiDictionaryButton.setBorder(compound);
		
		fiDictionaryButton.addMouseListener(new MouseListener() {

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				new GUIDictionaryForm(true, client);
				
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}
		});
		
		enDictionaryButton = new JButton("Show FI-EN DICTIONARY");
		enDictionaryButton.setForeground(Color.BLACK);
		enDictionaryButton.setBackground(Color.YELLOW);
		enDictionaryButton.setBorder(compound);
		
		enDictionaryButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
				new GUIDictionaryForm(false, client);
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}});
		
		translatePanel.add(labelStatus);
		translatePanel.add(comboBoxStatus);
		//translatePanel.add(labelWord);
		translatePanel.add(txt_Word);
		translatePanel.add(translateButton);
		translatePanel.add(labelWord);
		translatePanel.add(txt_Translated);
		
		translatePanel.add(emptyLabel2);
		translatePanel.add(emptyLabel3);
		translatePanel.add(fiDictionaryButton);
		translatePanel.add(enDictionaryButton);
	}
}