package client;

import handlers.Dictionary;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
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

import javax.swing.ImageIcon;
import javax.swing.JButton;
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

public class GUIDictionaryForm extends JFrame implements ActionListener {

	Client client = null;
	
	Container contentPane;
	JTable table = null;

	JScrollPane scrollPane = null;

	public GUIDictionaryForm(Boolean toFi, Client client) {  // toFi - if true translate from ENG to FI
		super("Dictionary Form");
		
		this.client = client;

		contentPane = this.getContentPane();
		
		
		table = new JTable(dictionaryData(toFi), dictionaryHeader(toFi));
		
		table.setCellSelectionEnabled(true);

		ListSelectionModel cellSelectionModel = table.getSelectionModel();

		cellSelectionModel
			.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
		
		
		//contentPane = this.getContentPane();
		scrollPane = new JScrollPane(table);
		contentPane.add(scrollPane, BorderLayout.CENTER);
		

		setBackground(Color.cyan);

		//setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(400, 200);
		// Here we make the frame appear in the center of the window.
		setLocationRelativeTo(null);
		setVisible(true);
	}
	
	private String [][] dictionaryData(Boolean translateStatus_FI)
	{
		String[][] rowData = null;
		
		if (translateStatus_FI)
		{
			String w1 = "Thank you";
			String w2 = "Hello";
			String w3 = "Good day";
			String w4 = "It is a beautiful day";
			String w5 = "I am sorry";
			
			rowData = new String[][] { {w1, client.translateToFi(w1)},
					{ w2, client.translateToFi(w2)}, { w3, client.translateToFi(w3)},
					{ w4, client.translateToFi(w4)}, { w5, client.translateToFi(w5)}};
		}
		else
		{
			String w1 = "Hei";
			String w2 = "Hyv‰‰ p‰iv‰‰";
			String w3 = "N‰hd‰‰n";
			
			rowData = new String[][] { {w1, client.translateToEng(w1)},
					{ w2, client.translateToEng(w2)}, { w3, client.translateToEng(w3)} };
		}
		
		return rowData;
	}
	
	private String [] dictionaryHeader(Boolean translateStatus_FI)
	{
		String[] colHeads = null;
		
		if (translateStatus_FI)
		{
			colHeads = new String[] { "ENG", "FI"};
		}
		else
		{
			colHeads = new String[] { "FI", "ENG"};
		}
		
		return colHeads;
	}

	@Override
	public void actionPerformed(ActionEvent arg0) {
		// TODO Auto-generated method stub
		
	}
}